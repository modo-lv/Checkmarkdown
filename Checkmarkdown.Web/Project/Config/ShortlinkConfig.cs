namespace Checkmarkdown.Web.Project.Config;

public record ShortlinkConfig(
    IList<ShortlinkRewriteRule> RewriteRules, 
    IList<ShortlinkSite> Sites
);