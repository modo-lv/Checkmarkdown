using Checkmarkdown.Core.Ast.Processors;
using Checkmarkdown.Core.Elements;
using Checkmarkdown.Core.Utils;
using MoreLinq;

namespace Checkmarkdown.Core.Ast;

/// <summary>Runs a list of processors on a Checkmarkdown AST, manages build context, etc.</summary>
public class AstProcessorPipeline {
    // Internal queue is statically indexed, to easily match up with run order.
    private readonly AstProcessor?[] _queue = new AstProcessor[RunOrder.Count];

    /// <summary>Currently added AST processors in the order they will run.</summary>
    public IReadOnlyList<AstProcessor> Queue =>
        this._queue.Where(it => it != null).ToList().AsReadOnly()!;

    /// <summary>Add an AST processor to the pipeline queue.</summary>
    /// <remarks>
    /// The correct order (<see cref="RunOrder"/>) of processors in the queue is ensured automatically.
    /// To see the run order, check <see cref="Queue"/>.
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

    public Document Run(String input) {
        var doc = FromMarkdown.ToCheckmarkdown(input);
        Queue.ForEach(it => it.ProcessRecursively(doc));
        return doc;
    }
    
    

    /// <summary>List of processor types in the order that they should be run.</summary>
    public static readonly IReadOnlyList<Type> RunOrder = [
        typeof(ListItemProcessor),
    ];

    /// <summary>
    /// Returns a new <see cref="AstProcessorPipeline"/> instance with all known AST processors queued.
    /// </summary>
    public static AstProcessorPipeline CreateDefault() {
        return new AstProcessorPipeline()
            .Add(new ListItemProcessor());
    }

}