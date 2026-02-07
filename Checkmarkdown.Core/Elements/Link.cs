using Markdig.Syntax.Inlines;
using Inline = Checkmarkdown.Core.Elements.Meta.Inline;

namespace Checkmarkdown.Core.Elements;

public class Link : Inline
{
    /// <summary>Is this a link to an external resource?</summary>
    public Boolean IsExternal { get; protected set; }
    /// <summary>Is this a project link to another element in another document?</summary>
    public Boolean IsInternal => !this.IsExternal;

    /// <summary>Link target.</summary>
    public String Target {
        get => field;
        set {
            field = value;
            this.IsExternal = value.StartsWith('/') || value.StartsWith('@') || value.Contains("://");
        }
    } = "";


    public Link() { }
    /// <inheritdoc />
    public Link(LinkInline link) : base(link) {
        this.Target = link.Url!;
    }
}