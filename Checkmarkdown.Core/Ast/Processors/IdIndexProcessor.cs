using Checkmarkdown.Core.Elements;
using Checkmarkdown.Core.Elements.Meta;

namespace Checkmarkdown.Core.Ast.Processors;

/// <summary>Populates the ID-document index.</summary>
public class IdIndexProcessor : AstProcessor {

    public override Element Process(Element node) {
        if (node is Document doc) {
            Storage = doc; // Set/update current document.
            return node;
        }

        if (node.ExplicitId != null) {
            BuildContext.IdIndex[Globals.Id(node.ExplicitId)] = (Document)Storage!;
        }

        return node;
    }
    
}