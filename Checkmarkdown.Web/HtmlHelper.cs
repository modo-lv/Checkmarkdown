using System.Text;
using System.Text.Encodings.Web;
using Checkmarkdown.Core.Elements.Meta;
using Checkmarkdown.Core.Utils;
using MoreLinq;
using RazorLight.Text;

namespace Checkmarkdown.Web;

public static class HtmlHelper
{
    extension(Element self)
    {
        public RawString HtmlAttributes() {
            var attrs = new KeyValueMap();
            self.Attributes.Properties.ForEach(prop => {
                attrs.Add(prop.Key, prop.Value);
            });
            if (self.Attributes.Classes.Count > 0)
                attrs.Add("class", self.Attributes.Classes.JoinToString(separator: " "));
            if (self.ExplicitId != null)
                attrs.Add("id", self.ExplicitId);

            var result = "";
            if (attrs.Count > 0) {
                result = attrs
                    .Select(kv =>
                        $"{HtmlEncoder.Default.Encode(kv.Key)}=\"{HtmlEncoder.Default.Encode(kv.Value)}\""
                    )
                    .JoinToString(separator: " ");
            }
            return new RawString(result);
        }
    }
}