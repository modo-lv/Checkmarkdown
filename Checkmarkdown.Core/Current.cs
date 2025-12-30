using Checkmarkdown.Core.Project;

namespace Checkmarkdown.Core;

public static class Current {
    /// <summary>Global build context used when running a project build.</summary>
    /// <remarks>
    /// Having this as a static global simplifies code, including testing, but is only safe as long as builds
    /// are limited to one per app run. If any kind of concurrent builds and/or multiple builds per run are
    /// ever implemented, this will have to become an instance parameter.
    /// </remarks>
    public static ProjectBuildContext BuildContext {
        get => field ?? throw new NullReferenceException("Build context not set!");
        set;
    } = null;

}