using Checkmarkdown.Core.Elements.Meta;
using Checkmarkdown.Core.Project;
using Checkmarkdown.Core.Utils;
using MoreLinq.Extensions;

namespace Checkmarkdown.Core.Elements;

/// <summary>Root node of a Markdown/CMD page AST.</summary>
public class Document : BlockContainer
{
    public ProjectPath? SourceFile {
        get;
        set {
            field = value;
            ProjectFileId = value?.Relative.ToString()
                .Replace('\\', '/').TrimPrefix("pages/").TrimSuffix(".md");
            Depth = value?.Relative.Tokens.Length - 1 ?? 0;
            RootPathRelative = "../".Repeat(Depth).TrimSuffix("/");
        }
    }

    /// <summary>A unique document identifier, based on the source filename.</summary>
    public String? ProjectFileId { get; private set; }

    /// <summary>How deep in the project the source file for this document is.</summary>
    public Int32 Depth { get; private set; }

    /// <summary>Path to project root, relative to this document.</summary>
    /// <remarks>
    /// Use this to create links to other files, regardless of how deeply this document is nested. 
    /// </remarks>
    public String RootPathRelative { get; private set; } = "";

    public String PathToRoot(String projectPath) {
        return RootPathRelative + projectPath;
    }
}