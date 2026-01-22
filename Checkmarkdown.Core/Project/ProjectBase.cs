using Checkmarkdown.Core.Ast;
using Checkmarkdown.Core.Elements;
using MoreLinq;
using Serilog;
using Path = Fluent.IO.Path;

namespace Checkmarkdown.Core.Project;

/// <summary>Base class for Checkmarkdown projects.</summary>
public abstract class ProjectBase
{
    public readonly Path RootPath;

    /// <summary>Project's Checkmarkdown documents, processed into ASTs ready for output.</summary>
    public List<Document> Pages = [];

    protected ProjectBase(String rootPath) {
        this.RootPath = new(rootPath);
        Log.Information("Project directory: {dir}", this.RootPath.FullPath);
    }

    /// <summary>Find all Markdown files to compile into Checkmarkdown AST documents.</summary>
    public IEnumerable<ProjectPath> FindPageSources() {
        var pagePath = PathTo("pages");
        const String filter = "*.md";
        Log.Debug("Finding {filter} files in {path}", filter, pagePath.FullPath);
        return pagePath.Files(filter, recursive: true);
    }

    /// <summary>
    /// Builds a list of markdown sources into processed documents, and saves them to <see cref="Pages"/>.
    /// </summary>
    /// <param name="sources">An enumeration of files (paths) containing the markdown to process.</param>
    /// <returns><see cref="Pages"/></returns>
    public List<Document> BuildPages(IEnumerable<ProjectPath> sources) {
        sources.Select(it => $"{it.Full}").ForEach(source => {
            Log.Debug("Building checkmarkdown document: {file}", source);
            FromMarkdown.ToCheckmarkdown(
                Build.FileSystem.File.ReadAllText(source)
            );
        });

        return Pages;
    }

    public ProjectPath PathTo(String file) =>
        this.PathTo(new Path(file));

    public ProjectPath PathTo(Path file) =>
        new ProjectPath(this.RootPath, file);
}