using System.IO.Abstractions;
using Testably.Abstractions;

namespace Checkmarkdown.Web;

public static class App
{
    /// <summary>Global access to the current file system.</summary>
    /// <remarks>All logic should use this to work with files, to facilitate testing.</remarks>
    public static IFileSystem FileSystem {
        get => field ?? new RealFileSystem();
        set;
    }
}