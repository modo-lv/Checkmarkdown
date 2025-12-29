using Checkmarkdown.Core.Elements.Meta;
using Checkmarkdown.Core.Utils;
using Markdig.Renderers.Html;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Checkmarkdown.Core.Elements.Attributes;

/// <summary>
/// Holds HTML classes and arbitrary properties for an element (as provided by Markdig),
/// with additional ID recognition and support for CMD's `:property` shorthand.
/// </summary>
public class ElementAttributes {
    public readonly ISet<String> Classes = new HashSet<String>();

    public readonly IKeyValueMap Properties = new KeyValueMap();

    public ElementAttributes() { }

    /// <summary>Interprets property with the given key as a boolean.</summary>
    public Boolean Flag(String key) => 
        Boolean.Parse(this.Properties[key]);

    public ElementAttributes(HtmlAttributes attrs, Element element) {
        attrs.Id?.TakeUnless(it => it.IsWhiteSpace())?.Also(it =>
            element.ExplicitId = it
        );
        this.Classes = attrs.Classes?.ToHashSet() ?? [];
        attrs.Properties?.ForEach(prop => {
            // Shorthand syntax 
            if (prop.Key.StartsWith(':'))
                this.Properties[prop.Key[1..]] =
                    prop.Value.TakeUnless(it => it.IsWhiteSpace()) ?? "true";
            else
                this.Properties.Add(prop!);
        });
    }
}