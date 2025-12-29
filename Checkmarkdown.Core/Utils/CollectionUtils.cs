namespace Checkmarkdown.Core.Utils;

/// <summary>More readable shorthand for common string-key dictionaries.</summary>
public interface IStringMap<T> : IDictionary<String, T>;

/// <summary>More readable shorthand for common string-key dictionaries.</summary>
public class StringMap<T> : Dictionary<String, T>, IStringMap<T>;

/// <summary>More readable shorthand for common string-string dictionaries.</summary>
public interface IKeyValueMap : IDictionary<String, String>;

/// <summary>More readable shorthand for common string-string dictionaries.</summary>
public class KeyValueMap : Dictionary<String, String>, IKeyValueMap;

public static class CollectionUtils {
    extension(Object target) {
        public Boolean IsIn(params Object[] candidates) =>
            candidates.Contains(target);
    }

    extension(IEnumerable<Object> list) {
        /// <summary>Fluent variant on <see cref="String.Join{T}(String?, IEnumerable{T})"/>.</summary>
        public String JoinToString(String separator = ", ") =>
            String.Join(separator, list);
    }

    extension<T>(IReadOnlyList<T> list) {
        /// <summary>
        /// Finds the index of the first item in the list matching <paramref name="predicate"/>.
        /// </summary>
        /// <returns>Index of item, or <c>null</c> if not found.</returns>
        public Int32? FindIndex(Predicate<T> predicate) {
            for (var i = 0; i < list.Count; i++) {
                if (predicate(list[i]))
                    return i;
            }
            return null;
        }
    }
}