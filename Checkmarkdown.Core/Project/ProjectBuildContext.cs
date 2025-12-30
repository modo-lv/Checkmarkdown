namespace Checkmarkdown.Core.Project;

/// <summary>Holds additional data and storage used in building a project.</summary>
public class ProjectBuildContext {
    /// <summary>Arbitrary data storage for each AST processor.</summary>
    public readonly Dictionary<Type, Object> AstProcessorStorage = [];
}