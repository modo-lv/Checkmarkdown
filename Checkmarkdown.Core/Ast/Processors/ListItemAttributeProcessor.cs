using Checkmarkdown.Core.Elements;
using Checkmarkdown.Core.Elements.Meta;
using Checkmarkdown.Core.Project;

namespace Checkmarkdown.Core.Ast.Processors;

/// <remarks>
/// List items are generally handled well by Markdig, the only issue is with attributes.
/// <c>* Item {:attribute}</c> will attach to the paragraph element containing the text,
/// and not the <see cref="ListItem"/> element containing the paragraph.
/// So we fix that by moving the attributes up a level.
/// </remarks>
public class ListItemAttributeProcessor(ProjectBuildContext buildContext) : AstProcessor(buildContext)
{
    public override Element Process(Element node) {
        if (node is ListItem li)
            li.Children.FirstOrDefault()?.MoveAttributesTo(li);
        return node;
    }
}