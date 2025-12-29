using System.Diagnostics.CodeAnalysis;
using Checkmarkdown.Core.Elements.Attributes;
using Markdig.Renderers.Html;
using Markdig.Syntax;

namespace Checkmarkdown.Core.Elements.Meta;

/// <summary>Base class for all elements in the AST.</summary>
/// <remarks>
/// Do not inherit directly, use <see cref="Inline"/>, <see cref="Block"/> or <see cref="BlockContainer"/>.
/// </remarks>
[SuppressMessage("ReSharper", "PossibleInfiniteInheritance")]
public abstract class Element {
    /// <summary>All project-unique identifiers for this element.</summary>
    public HashSet<String> ExplicitIds = [];

    public ElementAttributes Attributes = new ElementAttributes();

    /// <summary>Sub-elements (children)</summary>
    public IList<Element> Children = [];


    /// <summary>Move <see cref="Attributes"/> from this element to another.</summary>
    /// <param name="element">Target element.</param>
    public virtual void MoveAttributesTo(Element element) {
        element.Attributes = this.Attributes;
        element.ExplicitIds = this.ExplicitIds;
        this.Attributes = new ElementAttributes();
        this.ExplicitIds = [];
    }


    /// <summary>Constructor.</summary>
    protected Element() {
        if (this is not Inline && this is not Block && this is not BlockContainer) {
            throw new NotSupportedException(
                $"Do not inherit {nameof(Element)} directly, " +
                $"use {nameof(Inline)}, {nameof(Block)}, or {nameof(BlockContainer)} instead."
            );
        }
    }

    /// <summary>Constructor, automatically copies attributes from the source Markdown object.</summary>
    protected Element(IMarkdownObject mdo) : this() {
        this.Attributes = new ElementAttributes(mdo.GetAttributes(), this);
    }
}