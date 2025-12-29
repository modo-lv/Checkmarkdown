using Markdig.Syntax;

namespace Checkmarkdown.Core.Elements.Meta;

/// <summary>Block element, must only contain other block elements.</summary>
public abstract class BlockContainer : Element {
    protected BlockContainer() { }
    protected BlockContainer(IMarkdownObject mdo) : base(mdo) { }
}