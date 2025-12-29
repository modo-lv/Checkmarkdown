using MarkdigText = Markdig.Syntax.Inlines.LiteralInline;
using Inline = Checkmarkdown.Core.Elements.Meta.Inline;

namespace Checkmarkdown.Core.Elements;

public class Text : Inline {
    public String Content;

    public Text(MarkdigText mdText) : base(mdText) =>
        Content = mdText.Content.ToString();
}