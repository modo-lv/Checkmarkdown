namespace Checkmarkdown.Core.Utils;

public static class StringUtils {
    extension(String? value) {
        /// <summary>Fluent alternative to <c>value ?? ""</c>.</summary>
        public String OrEmpty() =>
            value ?? "";
        
    }

    extension(String value) {
        public String TrimPrefix(String prefix) =>
            value.StartsWith(prefix) ? value[prefix.Length..] : value;
        
        public String TrimSuffix(String suffix) =>
            value.EndsWith(suffix) ? value[..^suffix.Length] : value;
    }
}