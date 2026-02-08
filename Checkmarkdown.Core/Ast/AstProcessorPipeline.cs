using Checkmarkdown.Core.Ast.Processors;
using Checkmarkdown.Core.Elements;
using Checkmarkdown.Core.Project;
using Checkmarkdown.Core.Utils;

namespace Checkmarkdown.Core.Ast;

/// <summary>Runs a list of processors on a Checkmarkdown AST, manages build context, etc.</summary>
public class AstProcessorPipeline
{
    // Internal queue is statically indexed, to easily match up with run order.
    private readonly AstProcessor?[] _queue = new AstProcessor[RunOrder.Count];

    /// <summary>Currently added AST processors in the order they will run.</summary>
    public IReadOnlyList<AstProcessor> Queue =>
        this._queue.Where(it => it != null).ToList().AsReadOnly()!;

    /// <summary>Add an AST processor to the pipeline queue.</summary>
    /// <remarks>
    /// The correct order (<see cref="RunOrder"/>) of processors in the queue is ensured automatically.
    /// To see the order that the processors have been queued in, check <see cref="Queue"/>.
    /// </remarks>
    public AstProcessorPipeline Add(AstProcessor processor) {
        var index =
            RunOrder.FindIndex(it => it == processor.GetType())
            ?? throw new InvalidOperationException(
                $"[{processor.GetType().Name}] type is not in the run order spec, can't add to pipeline."
            );
        if (_queue[index] != null)
            throw new InvalidOperationException(
                $"[{processor.GetType().Name}] already exists in the pipeline."
            );
        _queue[index] = processor;
        return this;
    }

    /// <summary>Runs the processor queue over the given documents.</summary>
    /// <param name="docs"></param>
    /// <returns>
    /// The pipeline queue is run in a horizontal way (each processor is run across all documents before
    /// moving on to the next). This is necessary because of processors like link index creator, which must
    /// index all the links in the project in order for the link processor to correctly differentiate between
    /// valid and missing shortlinks.
    /// </returns>
    public IList<Document> Run(IEnumerable<Document> docs) {
        return docs.Select(doc =>
            Queue.Aggregate(doc, (currentDoc, processor) =>
                (Document) processor.ProcessRecursively(currentDoc)
            )
        ).ToList();
    }


    /// <summary>List of processor types in the order that they should be run.</summary>
    public static readonly IList<Type> RunOrder = [
        // Attribute processors move attributes around the tree, so must not conflict with each other,
        // and must run before any processors that expect attributes to be correctly assigned.
        typeof(ListItemAttributeProcessor),
        typeof(DocumentAttributeProcessor),
        
        // Must run before anything that expects ID processing to be complete.
        typeof(ExplicitIdProcessor),
        // Must run after anything that might modify IDs.
        typeof(IdIndexProcessor),
        // Must run after ID index has been built and before any link processing.
        typeof(ImplicitShortlinkProcessor),
        // Heading-item processing is a prerequisite for correct implicit ID generation.
        typeof(HeadingItemProcessor),
        // Must run after anything that might modify IDs or element text.
        typeof(ImplicitIdProcessor),
    ];
    
}