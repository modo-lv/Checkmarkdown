using Checkmarkdown.Core.Markdown;
using FluentAssertions;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using Xunit;

namespace Checkmarkdown.Core.Tests.Markdown;

// ReSharper disable ArrangeTypeMemberModifiers
public class ShorthandLinkTests {
    [Fact] void BasicCase() {
        const String input = @"[x], y and z";
        var result = MarkdownParser.ParseToAst(input);
        var link = result.First().As<ParagraphBlock>().Inline!.First().As<LinkInline>();
        link.Url.Should().Be("");
        var text = link.Single().As<LiteralInline>();
        text.Content.Text.Should().Be("x");
    }

    [Fact] void Escaped() {
        const String input = @"\[x]";
        var result = MarkdownParser.ParseToAst(input);
        var text = result.First().As<ParagraphBlock>().Inline!.First();
        text.Should().NotBeOfType<LinkInline>();
    }

    [Fact] void ExplicitLink() {
        const String input = @"[x](y)";
        var result = MarkdownParser.ParseToAst(input);
        var link = result.First().As<ParagraphBlock>().Inline!.First();
        link.Should().BeOfType<LinkInline>();
        link.As<LinkInline>().Url.Should().Be("y");
    }

    [Fact] void ReferenceLink() {
        const String input = @"[x]

[x]: y";
        var result = MarkdownParser.ParseToAst(input);
        var link = result.First().As<ParagraphBlock>().Inline!.First();
        link.Should().BeOfType<LinkInline>();
        link.As<LinkInline>().Url.Should().Be("y");
    }
}