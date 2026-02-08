using Checkmarkdown.Core.Elements;
using Checkmarkdown.Core.Elements.Meta;
using Checkmarkdown.Core.Project;

namespace Checkmarkdown.Core.Ast.Processors;

/// <remarks>
/// Setting attributes on the whole document (not just its elements) is done by starting the document with
/// ":{..attributes..}". Markdig parses this as a paragraph with the attributes, so we move the attributes
/// to the document.
/// </remarks>
public class DocumentAttributeProcessor(ProjectBuildContext buildContext) : AstProcessor(buildContext)
{
    public override Element Process(Element node) {
        if (node is not Document) return node;
        
        if (node.Children.FirstOrDefault() is Paragraph p &&
            p.Children.FirstOrDefault() is Text { Content: ":" }) {
            p.MoveAttributesTo(node);
            node.Children.Remove(p);
        }
        
        return node;
    }
}