using Checkmarkdown.Core.Elements.Meta;
using Markdig.Syntax;

namespace Checkmarkdown.Core.Elements;

public enum ListingKind {
    Unordered,
    Ordered,
    /// <summary>
    /// Indicates a list item that should be output the same way as a checkbox item,
    /// but not actually have a checkbox.
    /// </summary>
    Items,
    /// <summary>A checkbox list item.</summary>
    CheckItems,
}

public class Listing : BlockContainer {
    public readonly ListingKind Kind = ListingKind.Unordered;

    public Listing(ListBlock mdo) : base(mdo) {
        if (mdo.IsOrdered)
            this.Kind = ListingKind.Ordered;
        else
            this.Kind = mdo.BulletType switch {
                '+' => ListingKind.CheckItems,
                '-' => ListingKind.Items,
                _ => this.Kind
            };
    }
}