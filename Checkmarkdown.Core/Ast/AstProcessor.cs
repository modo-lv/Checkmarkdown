using Checkmarkdown.Core.Elements.Meta;
using Checkmarkdown.Core.Project;
using Checkmarkdown.Core.Utils;

namespace Checkmarkdown.Core.Ast;

/// <summary>Base class for components that modify a Checkmarkdown AST in some way.</summary>
public abstract class AstProcessor
{
    protected static ProjectBuildContext BuildContext => Build.Context;

    /// <summary>Direct access to this processor's storage.</summary>
    public Object? Storage {
        get => BuildContext.AstProcessorStorage.Get(key: this.GetType());
        set => BuildContext.AstProcessorStorage.PutOrRemove(key: this.GetType(), value);
    }

    /// <summary>Typed access to stored data, including initialization.</summary>
    /// <param name="initValue">Initial value if storage is empty.</param>
    public T Stored<T>(T initValue) where T : notnull {
        return (T) (Storage ??= initValue);
    }

    /// <summary>Main functionality for processing and individual element node in the AST.</summary>
    public abstract Element Process(Element node);

    /// <summary>Runs <see cref="Process"/> on <paramref name="root"/> and all its children.</summary>
    public virtual Element ProcessRecursively(Element root) {
        var result = this.Process(root);
        result.Children = result.Children.Select(this.ProcessRecursively).ToList();
        return result;
    }
}