using Checkmarkdown.Core.Elements;
using Checkmarkdown.Core.Elements.Meta;
using MoreLinq.Extensions;

namespace Checkmarkdown.Core.Ast.Processors;

/// <summary>Converts headings into <see cref="Item"/>s.</summary>
/// <remarks>
/// This is necessary to correctly generate implicit IDs, which requires hierarchical document structure.  
/// </remarks>
/// <seealso cref="Item"/>
public class HeadingItemProcessor : AstProcessor {

    public override Element Process(Element node) {
        // Headings can't contain other headings.
        if (node is Heading) return node;
        
        Item? currentItem = null;
        Heading? lastHeading = null;
        var children = node switch {
            Item i => i.Content,
            _ => node.Children
        };
        var newChildren = new List<Element>();
        if (node is Item { Title: not null } it)
            newChildren.Add(it.Title);

        children.ForEach(child => {
            var newItem = child is Heading h && h.Level <= (lastHeading?.Level ?? 7);
            if (currentItem == null) {
                if (newItem)
                    currentItem = new Item(title: child);
                else
                    newChildren.Add(child);
            }
            else {
                if (newItem) {
                    newChildren.Add(currentItem);
                    currentItem = new Item(title: child);
                }
                else {
                    currentItem.Children.Add(child);
                }
            }

            if (newItem)
                lastHeading = child as Heading;
        });
        if (currentItem != null) {
            newChildren.Add(currentItem);
        }

        node.Children = newChildren;

        return node;
    }
}