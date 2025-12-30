using Checkmarkdown.Core.Utils;

namespace Checkmarkdown.Core;

public static class Globals {
    /// <summary>
    /// Prefix to when Forkdown-specific element attributes share a space with user-provided ones
    /// </summary>
    public const String Prefix = "fd--";

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