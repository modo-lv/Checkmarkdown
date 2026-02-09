using Checkmarkdown.Core.Elements.Meta;
using Markdig.Syntax;

namespace Checkmarkdown.Core.Elements;

/// <summary>An item in a numeric/bullet list.</summary>
/// <seealso cref="Item"/>
public class ListItem : BlockContainer
{
    public ListItem() { }
    public ListItem(IMarkdownObject mdo) : base(mdo) { }
}