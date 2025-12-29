using System.Text.RegularExpressions;

namespace Checkmarkdown.Core.Utils;

public static partial class RegexPatterns {
    [GeneratedRegex(@"\s")]
    public static partial Regex WhiteSpace { get; }
}