using Markdig.Syntax;

namespace Checkmarkdown.Core.Elements.Meta;

/// <summary>Inline element, must only contain other inline elements.</summary>
public abstract class Inline : Element {
    protected Inline() { }
    protected Inline(IMarkdownObject mdo) : base(mdo) { }
}