using Checkmarkdown.Core.Elements.Meta;
using Markdig.Syntax;

namespace Checkmarkdown.Core.Elements;

public enum ListingKind {
    Unordered,
    Ordered,
    /// <summary>A checkbox list item.</summary>
    Check,
    /// <summary>
    /// A list of items that should be output mostly the same way as checkboxes,
    /// but not actually have a checkbox.
    /// </summary>
    PseudoCheck,
}

public class Listing : BlockContainer {
    public readonly ListingKind Kind = ListingKind.Unordered;

    public Listing(ListBlock mdo) : base(mdo) {
        if (mdo.IsOrdered)
            this.Kind = ListingKind.Ordered;
        else
            this.Kind = mdo.BulletType switch {
                '+' => ListingKind.Check,
                '-' => ListingKind.PseudoCheck,
                _ => this.Kind
            };
    }
}