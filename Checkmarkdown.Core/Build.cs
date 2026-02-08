using System.IO.Abstractions;
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

}