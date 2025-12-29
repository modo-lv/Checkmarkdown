using Checkmarkdown.Core.Elements.Meta;
using Markdig.Syntax;

namespace Checkmarkdown.Core.Elements; 

public class ListItem : BlockContainer {
  public ListItem() { }
  public ListItem(IMarkdownObject mdo) : base(mdo) { }
}