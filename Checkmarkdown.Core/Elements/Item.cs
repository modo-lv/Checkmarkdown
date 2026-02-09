using Checkmarkdown.Core.Ast.Processors;
using Checkmarkdown.Core.Elements.Meta;

namespace Checkmarkdown.Core.Elements;

/// <summary>The central element of Checkmarkdown's functionality, used primarily for checkboxes.</summary>
/// <remarks>
/// Represents an item of information in the checklist. Usually a checkbox, but doesn't have to be.
/// Can have a title, be a checkbox, metadata (info/warning/help), and child elements.
///
/// Additionally, all <see cref="Heading"/>s are converted to <see cref="Item"/>s as well, to ensure a
/// hierarchical document structure (see <see cref="HeadingItemProcessor"/>). 
/// </remarks>
public class Item : BlockContainer {

    /// <summary>Is this a heading item?</summary>
    public readonly Boolean IsHeading;

    /// <summary>The first child element is the title.</summary>
    public Element? Title => Children.FirstOrDefault();

    /// <summary>Child elements, except <see cref="Title"/>.</summary>
    public IEnumerable<Element> Content => Children.Skip(1);

    /// <summary>Is this a checkbox-item?</summary>
    public readonly Boolean IsCheckItem = false;

    /// <summary>
    /// Creates an empty <see cref="Item"/> with the given <see cref="Element"/> as the title.
    /// </summary>
    public Item(Element title) {
        title.MoveAttributesTo(this);
        Children.Add(title);
        IsHeading = title is Heading;
    }

    /// <summary>Constructs an <see cref="Item"/> by converting a <see cref="ListItem"/>.</summary>
    public Item(ListItem sourceItem, ListingKind listingKind) {
        this.ExplicitId = sourceItem.ExplicitId;
        this.Attributes = sourceItem.Attributes;
        this.Children = sourceItem.Children;
        this.ImplicitId = sourceItem.ImplicitId;
        this.IsCheckItem = listingKind == ListingKind.CheckItem;
    }

}