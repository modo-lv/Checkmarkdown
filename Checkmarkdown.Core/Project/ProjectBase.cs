using Serilog;
using Path = Fluent.IO.Path;

namespace Checkmarkdown.Core.Project;

/// <summary>Base class for Checkmarkdown projects.</summary>
public abstract class ProjectBase
{
    public readonly Path RootPath;

    protected ProjectBase(String rootPath) {
        this.RootPath = new(rootPath);
        Log.Information("Project directory: {dir}", this.RootPath.FullPath);
    }

    public ProjectPath PathTo(String file) =>
        this.PathTo(new Path(file));

    public ProjectPath PathTo(Path file) =>
        new ProjectPath(this.RootPath, file);
}