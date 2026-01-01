using Checkmarkdown.Core.Elements;
using Checkmarkdown.Core.Elements.Meta;
using Checkmarkdown.Core.Utils;

namespace Checkmarkdown.Core.Ast.Processors;

/// <summary>Generates implicit IDs for <see cref="Item"/>s and <see cref="ListItem"/>s.</summary>
/// <remarks>
/// In order for any kind of output to automatically save and load checked items, each item must have a unique
/// ID. Adding an explicit ID manually for every checkbox would quickly become problematic for bigger
/// checklists, so this processor generates them based on the document filename, the item's position in the
/// heading and list hierarchy, and the textual content of the items.
/// <br/>
/// This is also why <see cref="HeadingItemProcessor"/> exists and must be run before this. 
/// </remarks>
/// <example>
/// A checkbox in a second level list under a heading in a document at <c>pages/chapter1/page1.md</c> will
/// have this implicit ID: <code>chapter1/page1␝Heading␝Parent⸱List⸱Item␝Child⸱List⸱Item</code>
/// Repeated items in the same list get a <c>␞2</c>, <c>␞3</c>, etc. suffix.
/// </example>
public class ImplicitIdProcessor : AstProcessor {
    public const Char GroupSep = '␝'; // Group separator
    public const Char RecordSep = '␞'; // Record separator
    public const Char WordSep = '⸱'; // Word separator

    private Dictionary<String, Int32> _times = [];

    public override Element Process(Element node) => throw new NotImplementedException(
        $"[{GetType().Name}] has a custom [{nameof(this.ProcessRecursively)}] implementation, " +
        $"this should not be called."
    );

    public override Element ProcessRecursively(Element node) {
        this._times = [];
        return this.Process(node, node.ExplicitId ?? node.ImplicitId);
    }

    protected Element Process(Element element, String? parentId) {
        var id = parentId;

        if (element is Document { ProjectFileId: not null } doc)
            id = doc.ProjectFileId;
        else if (element.ExplicitId != null) {
            id = element.ExplicitId;
        } else if (element is Item or ListItem) {
            id = element.TitleText.TakeUnless(it => it.IsWhiteSpace())?.Trim().Replace(' ', WordSep)
                 ?? element.GetType().Name;

            if (parentId != null)
                id = $"{parentId}{GroupSep}{id}";
            this._times[id] = this._times.GetValueOrDefault(id) + 1;
            if (this._times.GetValueOrDefault(id) > 1)
                id += $"{RecordSep}{this._times[id]}";
            element.ImplicitId = id;
        }

        element.Children = element.Children.Select(child =>
            this.Process(element: child, parentId: id)
        ).ToList();

        return element;
    }
}