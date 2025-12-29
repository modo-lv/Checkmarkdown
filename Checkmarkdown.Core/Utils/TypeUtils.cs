namespace Checkmarkdown.Core.Utils;

public static class TypeUtils {
    extension(Object receiver) {
        /// <summary>Fluent cast to a type.</summary>
        public T As<T>() where T : class => (T) receiver;
    }
}