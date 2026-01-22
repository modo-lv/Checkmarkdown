using Checkmarkdown.Core.Elements.Meta;
using Checkmarkdown.Core.Project;
using Checkmarkdown.Core.Utils;

namespace Checkmarkdown.Core.Elements;

/// <summary>Root node of a Markdown/CMD page AST.</summary>
public class Document : BlockContainer
{
    /// <summary>Path of the file that this document was created from, relative to project root.</summary>
    /// <remarks>
    /// Uses forward slashes regardless of OS. Can be empty if the source didn't come from a file.
    /// </remarks>
    public String SourcePath { get; private set; } = "";

    public void SetSourceFile(ProjectPath? path) {
        SourcePath = path?.FullPath.Replace('\\', '/') ?? "";
    }

    /// <summary>A unique document identifier, based on the source filename.</summary>
    public String? ProjectFileId =>
        this.SourcePath.TakeUnless(it => it.IsWhiteSpace())?.TrimPrefix("pages/").TrimSuffix(".md");

    /// <summary>How deep in the project the source file for this document is.</summary>
    public Int32 Depth => this.SourcePath.Count(it => it == '/');
}