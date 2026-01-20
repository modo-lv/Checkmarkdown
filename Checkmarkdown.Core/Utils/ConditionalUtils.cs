namespace Checkmarkdown.Core.Utils;

public static class ConditionalUtils {
    extension<T>(T value) where T : class {
        /// <summary>Kotlin-style shorthand for returning null if the value doesn't match a predicate.</summary>
        public T? TakeIf(Func<T, Boolean> predicate) => 
            predicate(value) ? value : null;
        
        /// <summary>Kotlin-style shorthand for returning null if the value matches a predicate.</summary>
        public T? TakeUnless(Func<T, Boolean> predicate) => 
            predicate(value) ? null : value;
    }
}