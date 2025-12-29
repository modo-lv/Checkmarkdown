using System.Diagnostics.CodeAnalysis;
using Checkmarkdown.Core.Elements.Attributes;
using Checkmarkdown.Core.Utils;
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

    public ElementAttributes Attributes = new();

    /// <summary>Sub-elements (children)</summary>
    public IList<Element> Children = [];

    /// <summary>Title text of the element (from the first text line in the top descendant chain)</summary>
    /// <remarks>
    /// Calculates the title on first access, and caches it. Set to <c>null</c> to re-calculate.
    /// If a suitable text line cannot be found, will default to an empty string.
    /// </remarks>
    public String TitleText {
        get => field ??= TitleTextOf(this, found: Children.FirstOrDefault() is Inline);
        set;
    } = null;


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

    /// <summary>
    /// Extract an element's title (first line in plain text) from its top descendant chain.
    /// </summary>
    /// <param name="element">Element.</param>
    /// <param name="found">When <c>true</c>, indicates that the first text-containing descendant has been
    /// found and title construction is underway.</param>
    /// <returns></returns>
    protected static String TitleTextOf(Element element, Boolean found = false) {
        if (element is Text text)
            return text.Content;

        if (found) {
            return element.Children
                .TakeWhile(it => it is Inline and not LineBreak)
                .Select(it => TitleTextOf(it, found))
                .JoinToString("");
        }

        return element.Children.FirstOrDefault()?.Let(it =>
            TitleTextOf(it, found: it is Inline)
        ) ?? "";
    }
}