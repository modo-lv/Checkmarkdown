// ReSharper disable NotAccessedField.Global

using MarkdigEmphasis = Markdig.Syntax.Inlines.EmphasisInline;
using Checkmarkdown.Core.Elements.Meta;

namespace Checkmarkdown.Core.Elements;

public class Emphasis(MarkdigEmphasis input) : Inline(input) {
    public Boolean IsStrong = input.DelimiterCount > 1;
}