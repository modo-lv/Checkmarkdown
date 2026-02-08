using Newtonsoft.Json;

namespace Checkmarkdown.Web.Project.Config;

/// <summary>
/// A rule indicating how to modify the shortlink URL to correctly append to the base URl of the site.
/// </summary>
/// <param name="Pattern">Regex pattern to look for.</param>
/// <param name="Replacement">Replacement string.</param>
[JsonConverter(typeof(ShortlinkRewriteRuleConverter))]
public record ShortlinkRewriteRule(
    String Pattern,
    String Replacement
);