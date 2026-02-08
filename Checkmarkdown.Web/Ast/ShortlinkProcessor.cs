using System.Text.RegularExpressions;
using Checkmarkdown.Core.Ast;
using Checkmarkdown.Core.Elements;
using Checkmarkdown.Core.Elements.Meta;
using Checkmarkdown.Core.Utils;
using Checkmarkdown.Web.Project;
using MoreLinq;

namespace Checkmarkdown.Web.Ast;

public class ShortlinkProcessor(WebBuildContext buildContext) : AstProcessor(buildContext)
{
    public override Element Process(Element node) {
        if (node is Link link && link.Target.StartsWith('@')) {
            link.Target = link.Target[1..];
            // General rewrites
            buildContext.Config.Shortlinks?.RewriteRules?.ForEach(rule => {
                link.Target = Regex.Replace(link.Target, rule.Pattern, rule.Replacement);
            });
            // Specific rewrites
            buildContext.Config.Shortlinks?.Sites?.FirstOrDefault()?.Also(site => {
                site.RewriteRules?.ForEach(rule => {
                    link.Target = Regex.Replace(link.Target, rule.Pattern, rule.Replacement);
                });
                link.Target = site.BaseUrl + Uri.EscapeDataString(link.Target);
            });
        }
        return node;
    }
}