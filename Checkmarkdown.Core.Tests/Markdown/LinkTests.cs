using FluentAssertions;
using Markdig.Parsers;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Checkmarkdown.Core.Tests.Markdown;

public class LinkTests {
    [Fact] void ExplicitExternalShorthand() {
        const String input = "[x](@)";

        var result = MarkdownParser.Parse(input);
        var link = result.Single().As<ParagraphBlock>().Inline!.Single().As<LinkInline>();

        link.Url.Should().Be("@");
    }
}