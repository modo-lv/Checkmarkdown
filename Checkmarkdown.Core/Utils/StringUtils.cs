using System.Text;

namespace Checkmarkdown.Core.Utils;

public static class StringUtils {
    extension(String? value) {
        /// <summary>Fluent alternative to <c>value ?? ""</c>.</summary>
        public String OrEmpty() =>
            value ?? "";
        
    }

    extension(String value) {
        /// <summary>
        /// Repeat a given <see cref="T:System.String" /> a number of times.
        /// </summary>
        /// <param name="times">Number of times to repeat.</param>
        /// <returns>Resulting repeated text.</returns>
        public String Repeat(Int32 times)
        {
            if (times < 1)
                return String.Empty;
            var stringBuilder = new StringBuilder();
            for (var index = 0; index < times; index++)
                stringBuilder.Append(value);
            return stringBuilder.ToString();
        }
        
        public String TrimPrefix(String prefix) =>
            value.StartsWith(prefix) ? value[prefix.Length..] : value;
        
        public String TrimSuffix(String suffix) =>
            value.EndsWith(suffix) ? value[..^suffix.Length] : value;
    }
}