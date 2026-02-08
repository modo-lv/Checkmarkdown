namespace Checkmarkdown.Web.Project.Config;

/// <summary>
/// An external site that a shortlink can lead to.
/// </summary>
/// <param name="Label">Short (1-2 chars) label to use for the link.</param>
/// <param name="Title">A more descriptive title of the site, used as a tooltip on the link label.</param>
/// <param name="BaseUrl">The root URL of the external site</param>
public record ShortlinkSite(
    String Label,
    String? Title,
    String BaseUrl,
    IList<ShortlinkRewriteRule>? RewriteRules
);