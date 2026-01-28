using Checkmarkdown.Core.Elements.Meta;
using Checkmarkdown.Core.Project;
using Checkmarkdown.Core.Utils;

namespace Checkmarkdown.Core.Elements;

/// <summary>Root node of a Markdown/CMD page AST.</summary>
public class Document : BlockContainer
{
    public ProjectPath? SourceFile = null;

    /// <summary>A unique document identifier, based on the source filename.</summary>
    public String? ProjectFileId =>
        this.SourceFile?.Relative.ToString().Replace('\\', '/').TrimPrefix("pages/").TrimSuffix(".md");

    /// <summary>How deep in the project the source file for this document is.</summary>
    public Int32 Depth => this.SourceFile?.Relative.ToString().Count(it => it == '/') ?? 0;
}