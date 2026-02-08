using Markdig.Syntax.Inlines;
using Inline = Checkmarkdown.Core.Elements.Meta.Inline;

namespace Checkmarkdown.Core.Elements;

public class Link(LinkInline input) : Inline
{
    /// <summary>Is this a link to an external resource?</summary>
    public Boolean IsExternal { get; private set; }
    /// <summary>Is this a project link to another element in another document?</summary>
    public Boolean IsInternal { get; private set; }

    /// <summary>Link target.</summary>
    public String Target {
        get;
        set {
            field = value;
            this.IsInternal = value.StartsWith('#');
            this.IsExternal = !this.IsInternal;
        }
    } = input.Url ?? "";
}