namespace Checkmarkdown.Core.Utils;

public static class StringUtils {
    extension(String? value) {
        /// <summary>Fluent alternative to <c>value ?? ""</c>.</summary>
        public String OrEmpty() =>
            value ?? "";
    }
}