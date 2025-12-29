namespace Checkmarkdown.Core.Utils;

public static class ConditionalUtils {
    extension<T>(T value) {
        /// <summary>Kotlin-style shorthand for returning null if the value matches a predicate.</summary>
        public T? TakeUnless(Func<T, Boolean> predicate) => 
            predicate(value) ? default : value;
    }
}