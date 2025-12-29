using Markdig.Syntax;

namespace Checkmarkdown.Core.Elements.Meta;

/// <summary>Block element, must only contain inline elements.</summary>
public abstract class Block : Element {
    protected Block() { }
    protected Block(IMarkdownObject mdo) : base(mdo) { }
}