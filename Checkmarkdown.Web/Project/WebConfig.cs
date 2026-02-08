using Checkmarkdown.Web.Project.Config;

namespace Checkmarkdown.Web.Project;

public record WebConfig(
    Guid ProjectId,
    ShortlinkConfig? Shortlinks
);