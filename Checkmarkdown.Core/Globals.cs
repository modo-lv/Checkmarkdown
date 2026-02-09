using Checkmarkdown.Core.Utils;

namespace Checkmarkdown.Core;

public static class Globals {
    /// <summary>Convert a string to a valid global ID.</summary>
    /// <param name="input">String to convert.</param>
    /// <returns>A syntactically valid global ID.</returns>
    public static String Id(String? input) {
        return input.OrEmpty().Trim()
            .Let(it => RegexPatterns.WhiteSpace.Replace(it, "_"))
            .Replace("'", "_")
            .Replace("\"", "_")
            .ToLowerInvariant();
    }
}