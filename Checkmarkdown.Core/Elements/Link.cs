using Inline = Checkmarkdown.Core.Elements.Meta.Inline;

namespace Checkmarkdown.Core.Elements;

public class Link : Inline
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
            this.IsExternal = value.StartsWith('/') || value.StartsWith('@') || value.Contains("://");
            this.IsInternal = value.StartsWith(':');
        }
    } = "";
}