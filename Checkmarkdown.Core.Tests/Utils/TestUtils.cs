using Checkmarkdown.Core.Elements.Meta;

namespace Checkmarkdown.Core.Tests.Utils;

public static class TestUtils {
    extension(Element node) {
        /// <summary>Return the first descendant node of a given type.</summary>
        public T FirstDescendant<T>() where T : Element =>
            node.FindDescendant<T>()!
            ?? throw new NullReferenceException($"Node has no children of type {typeof(T).Name}.");

        /// <summary>Look for the first descendant node of a given type.</summary>
        public T? FindDescendant<T>() where T : Element =>
            node.Children.FirstOrDefault(it => it is T) as T
            ?? node.Children.Select(FindDescendant<T>).FirstOrDefault(it => it != null);
    }
}