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

    extension<TKey, TValue>(IDictionary<TKey, TValue> dict) where TValue : class {
        /// <summary>Returns the value with the given key, or <c>null</c> if it's not set.</summary>
        public TValue? Get(TKey key) =>
            dict.TryGetValue(key, out var value) ? value : null;
    }

    extension<TKey, TValue>(IDictionary<TKey, TValue> dict) where TValue : class {
        /// <summary>
        /// Updates the value at <paramref name="key"/>, or removes it if the new value is <c>null</c>.
        /// </summary>
        public void PutOrRemove(TKey key, TValue? value) {
            if (value != null)
                dict[key] = value;
            else dict.Remove(key);
        }
    }

    extension<T>(IList<T> list) {
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