using Checkmarkdown.Core.Elements.Meta;
using Checkmarkdown.Core.Utils;

namespace Checkmarkdown.Core.Elements;

/// <summary>Root node of a Markdown/CMD page AST.</summary>
public class Document : BlockContainer {
    /// <summary>Path of the file that this document was created from, relative to project root.</summary>
    public String ProjectFilePath {
        get;
        set => field = value.Replace('\\', '/');
    } = "";

    /// <summary>A unique document identifier, based on the filename.</summary>
    public String ProjectFileId => this.ProjectFilePath.TrimPrefix("pages/").TrimSuffix(".md");

    /// <summary>How deep in the project file tree this document is.</summary>
    public Int32 Depth => this.ProjectFilePath.Count(it => it == '/');
}