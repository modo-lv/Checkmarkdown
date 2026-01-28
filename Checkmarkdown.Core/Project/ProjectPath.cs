using Checkmarkdown.Core.Utils;
using Path = Fluent.IO.Path;

namespace Checkmarkdown.Core.Project;

/// <summary>Provides convenient reference to a project file path.</summary>
public class ProjectPath
{
    /// <summary>Project root path.</summary>
    public readonly Path Root;

    /// <summary>File path relative to project root.</summary>
    public readonly Path Relative;

    /// <summary>Full file path, including project root.</summary>
    public readonly Path Full;

    /// <summary>Does this path exist in the filesystem?</summary>
    public readonly Boolean Exists;

    public readonly String FullPath;

    /// <summary><see cref="Relative"/> path with an added trailing directory separator.</summary>
    public readonly String DirPath;

    public ProjectPath(Path root, Path relativeFilePath) {
        Root = root;
        Relative = relativeFilePath.IsRooted ? relativeFilePath.MakeRelativeTo(root) : relativeFilePath;
        Full = this.Root.Combine(this.Relative);
        Exists = Full.Exists;
        FullPath = Full.ToString();
        DirPath = $"{Relative}{System.IO.Path.DirectorySeparatorChar}";
    }

    public override String ToString() =>
        Full.IsDirectory ? DirPath : $"{Relative}";

    public IEnumerable<ProjectPath> Files(String filter, Boolean recursive) =>
        Full.Files(filter, recursive).Select(it => new ProjectPath(Root, it));

    public Path Combine(String path) => Full.Combine(path);
}