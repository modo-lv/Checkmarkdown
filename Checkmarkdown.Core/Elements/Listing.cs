using Checkmarkdown.Core.Elements.Meta;
using Checkmarkdown.Core.Utils;
using Markdig.Syntax;

namespace Checkmarkdown.Core.Elements;

public enum ListingKind
{
    Unordered,
    Ordered,
    /// <summary>Listing contains checkbox items.</summary>
    /// <example><c>+ item</c></example>
    CheckItem,
    /// <summary>
    /// A list of items that should be output similar to <see cref="CheckItem"/>s,
    /// but not actually have a checkbox.
    /// </summary>
    /// <example><c>- item</c></example>
    Item,
}

public class Listing : BlockContainer
{
    public readonly ListingKind Kind = ListingKind.Unordered;

    /// <summary>
    /// Is this a <see cref="ListingKind.Item"/> or <see cref="ListingKind.CheckItem"/> list?
    /// </summary>
    public readonly Boolean IsItemList;

    public Listing(ListBlock mdo) : base(mdo) {
        if (mdo.IsOrdered)
            this.Kind = ListingKind.Ordered;
        else {
            this.Kind = mdo.BulletType switch {
                '+' => ListingKind.CheckItem,
                '-' => ListingKind.Item,
                _ => this.Kind
            };
            IsItemList = this.Kind.IsIn(ListingKind.Item, ListingKind.CheckItem);
        }
    }
}