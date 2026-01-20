using Path = Fluent.IO.Path;

namespace Checkmarkdown.Core.Project;

/// <summary>Base class for Checkmarkdown projects.</summary>
public abstract class ProjectBase(Path rootPath)
{
    public Path RootPath = rootPath;

    public ProjectPath PathTo(String file) =>
        this.PathTo(new Path(file));

    public ProjectPath PathTo(Path file) =>
        new ProjectPath(this.RootPath, file);
}