using Checkmarkdown.Core.Ast;
using Checkmarkdown.Core.Elements;
using Checkmarkdown.Core.Elements.Meta;
using Checkmarkdown.Core.Utils;
using MoreLinq;
using Serilog;
using Path = Fluent.IO.Path;

namespace Checkmarkdown.Core.Project;

/// <summary>Base class for Checkmarkdown projects.</summary>
public abstract class ProjectBase
{
    public readonly Path RootPath;

    /// <summary>Project's Checkmarkdown documents, processed into ASTs ready for output.</summary>
    public readonly List<Document> Documents = [];

    protected ProjectBase(String rootPath) {
        this.RootPath = new(rootPath);
        Log.Information("Project directory: {dir}", this.RootPath.FullPath);
    }

    /// <summary>Find all Markdown files to compile into Checkmarkdown AST documents.</summary>
    public IList<ProjectPath> FindPages() {
        var pagePath = PathTo("pages");
        if (!pagePath.Exists)
            AppUtils.FatalExit("Missing project page source: {pages}", pagePath.DirPath);
        const String filter = "*.md";
        var files = pagePath.Files(filter, recursive: true).ToList();
        if (files.Count == 0)
            AppUtils.FatalExit("There are no {filter} files in: {pages}.", filter, pagePath);
        Log.Information(
            "Found {count} {filter} file(s) in: {path}", files.Count, filter, pagePath
        );
        return files;
    }

    /// <summary>
    /// Builds a list of markdown sources into processed documents, and saves them to <see cref="Documents"/>.
    /// </summary>
    /// <param name="pages">An enumeration of files (paths) containing the markdown to process.</param>
    /// <param name="pipeline">Pipeline to use for building the pages.</param>
    /// <returns><see cref="Documents"/></returns>
    public IList<Document> BuildDocuments(IList<ProjectPath> pages, AstProcessorPipeline pipeline) {
        var docs = pages.Select(page => {
            Log.Information("Building Checkmarkdown document: {file}", page);
            return FromMarkdown.ToCheckmarkdown(Build.FileSystem.File.ReadAllText(page.FullPath), page);
        });
        pipeline.Run(docs).ForEach(Documents.Add);

        return Documents;
    }

    /// <summary>Creates a <see cref="ProjectPath"/> object for a project file/dir.</summary>
    /// <param name="pathParts">
    /// The path to the file/dir, relative to project root. Either as a single string,
    /// or a list of path parts to be combined.
    /// </param>
    public ProjectPath PathTo(params IList<String> pathParts) =>
        this.PathTo(
            pathParts
                .Skip(1) // We're manually creating the initial path for the aggregate seed
                .Aggregate(seed: new Path(pathParts[0]), (path, nextPart) => path.Combine(nextPart))
        );

    public ProjectPath PathTo(Path file) =>
        new ProjectPath(this.RootPath, file);
}