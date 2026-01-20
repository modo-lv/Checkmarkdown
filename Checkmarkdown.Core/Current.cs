using System.IO.Abstractions;
using Checkmarkdown.Core.Project;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using Testably.Abstractions;

namespace Checkmarkdown.Core;

public static class Current
{
    public static void EnableLogging() {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console(
                theme: AnsiConsoleTheme.Code
            )
            .CreateLogger();
    }

    /// <summary>Global access to the current file system.</summary>
    /// <remarks>All logic should use this to work with files, to facilitate testing.</remarks>
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
    public static ProjectBuildContext BuildContext {
        get => field ?? throw new NullReferenceException("Build context not set!");
        set;
    } = null;

}