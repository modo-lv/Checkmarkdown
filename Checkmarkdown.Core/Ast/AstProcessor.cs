using Checkmarkdown.Core.Elements.Meta;

namespace Checkmarkdown.Core.Ast;

/// <summary>Base class for components that modify a Checkmarkdown AST in some way.</summary>
public abstract class AstProcessor {
    /// <summary>Main functionality for processing and individual element node in the AST.</summary>
    public abstract Element Process(Element node);

    /// <summary>Runs <see cref="Process"/> on <paramref name="root"/> and all its children.</summary>
    public virtual Element ProcessRecursively(Element root) {
        var result = this.Process(root);
        result.Children = result.Children.Select(this.ProcessRecursively).ToList();
        return result;
    }
}