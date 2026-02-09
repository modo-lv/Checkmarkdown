using Checkmarkdown.Core.Elements;
using Checkmarkdown.Core.Elements.Meta;
using Checkmarkdown.Core.Project;
using MoreLinq;

namespace Checkmarkdown.Core.Ast.Processors;

/// <summary>
/// Unwraps <see cref="Listing"/>s of <see cref="ListingKind.Item"/>s and
/// <see cref="ListingKind.CheckItem"/>s, converting their <see cref="ListItem"/>s into <see cref="Item"/>s
/// and making them direct children of the containing element.
/// </summary>
public class ItemListUnwrapProcessor(CoreBuildContext buildContext) : AstProcessor(buildContext)
{
    public override Element Process(Element node) {
        if (node is BlockContainer && node.Children.Any(it => it is Listing { IsItemList: true })) {
            var newChildren = new List<Element>();
            node.Children.ForEach(child => {
                if (child is Listing { IsItemList: true } list) {
                    list.Children.Select(it => (ListItem) it).ForEach(listItem => {
                        newChildren.Add(new Item(listItem, list.Kind));
                    });
                } else newChildren.Add(child);
            });
            node.Children = newChildren;
        }
        return node;
    }
}