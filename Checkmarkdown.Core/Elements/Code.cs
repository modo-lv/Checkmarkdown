using Markdig.Syntax.Inlines;
using Inline = Checkmarkdown.Core.Elements.Meta.Inline;

namespace Checkmarkdown.Core.Elements;

/// <summary>An inline code block: <c>`x`</c>.</summary>
public class Code(CodeInline input) : Inline
{
    public String Content = input.Content;
}