using Checkmarkdown.Core.Elements;
using Checkmarkdown.Core.Elements.Meta;
using Checkmarkdown.Core.Project;

namespace Checkmarkdown.Core.Ast.Processors;

/// <summary>Populates the ID-document index.</summary>
public class IdIndexProcessor(ProjectBuildContext buildContext) : AstProcessor(buildContext)
{
    public override Element Process(Element node) {
        if (node is Document doc) {
            Storage = doc; // Set/update current document.
            return node;
        }

        if (node.ExplicitId != null) {
            buildContext.IdIndex[node.ExplicitId] = (Document)Storage!;
        }

        return node;
    }
    
}