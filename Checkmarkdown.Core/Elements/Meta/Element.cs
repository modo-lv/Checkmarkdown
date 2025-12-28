using System.Diagnostics.CodeAnalysis;

namespace Checkmarkdown.Core.Elements.Meta;

/// <summary>Base class for all elements in the AST.</summary>
/// <remarks>
/// Do not inherit directly, use <see cref="Inline"/>, <see cref="Block"/> or <see cref="BlockContainer"/>.
/// </remarks>
[SuppressMessage("ReSharper", "PossibleInfiniteInheritance")]
public abstract class Element {
    /// <summary>Sub-elements (children)</summary>
    public IList<Element> Children = [];

    /// <summary>Constructor, prevents arbitrary inheritance.</summary>
    protected Element() {
        if (this is not Inline && this is not Block && this is not BlockContainer) {
            throw new NotSupportedException(
                $"Do not inherit {nameof(Element)} directly, " +
                $"use {nameof(Inline)}, {nameof(Block)}, or {nameof(BlockContainer)} instead."
            );
        }
    }
}