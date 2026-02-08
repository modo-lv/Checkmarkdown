using Checkmarkdown.Core.Project;

namespace Checkmarkdown.Web.Project;

public class WebBuildContext(WebConfig config) : CoreBuildContext
{
    public readonly WebConfig Config = config;
}