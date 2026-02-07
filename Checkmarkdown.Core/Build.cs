using System.IO.Abstractions;
using Checkmarkdown.Core.Project;
using Testably.Abstractions;

namespace Checkmarkdown.Core;

public static class Build
{
    /// <summary>Global access to the file system used to load checkmarkdown files to build.</summary>
    /// <remarks>All build logic should use this to work with files, to facilitate testing.</remarks>
    public static IFileSystem FileSystem {
        get => field ?? new RealFileSystem();
        set;
    }

    /// <summary>Global build context used when running a project build.</summary>
    /// <remarks>
    /// Having this as a static global simplifies code, including testing, but is only safe as long as builds
    /// are limited to one per app run. If any kind of concurrent builds and/or multiple builds per run are
    /// ever implemented, this will have to become an instance parameter.
    /// </remarks>
    public static ProjectBuildContext Context { get; set; } = new();

}