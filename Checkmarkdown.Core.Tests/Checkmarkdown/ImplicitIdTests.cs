using Checkmarkdown.Core.Ast;
using Checkmarkdown.Core.Ast.Processors;
using Checkmarkdown.Core.Elements;
using Checkmarkdown.Core.Tests.Utils;
using Checkmarkdown.Core.Tests.Wiring;
using Checkmarkdown.Core.Utils;
using FluentAssertions;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Checkmarkdown.Core.Tests.Checkmarkdown;

public class ImplicitIdTests : TestServices {
    private AstProcessorPipeline Pipeline => TestScope.FullCoreAstPipeline();

    [Fact] void NestedWithHeading() {
        const String input =
            """
            + # Heading
              + One
              + Other
            """;
        var result = Pipeline.RunFromMarkdown(input);
        result
            .FirstDescendant<ListItem>()
            .FirstDescendant<ListItem>()
            .GlobalId.Should().Be($"Heading{ImplicitIdProcessor.GroupSep}One");
    }

    [Fact]
    void TrimSpaces() {
        const String input = "* Heading {something}";
        var result = Pipeline.RunFromMarkdown(input);
        result.FirstDescendant<ListItem>().ImplicitId.Should().Be("Heading");
    }

    [Fact] void ExplicitId() {
        const String input =
            """
            # Heading One
            * Item
              * X {#xx}
                * Sub
            """;

        var result = Pipeline.RunFromMarkdown(input);
        result
            .FirstDescendant<ListItem>()
            .FirstDescendant<ListItem>()
            .FirstDescendant<ListItem>().ImplicitId.Should().Be($"xx{ImplicitIdProcessor.GroupSep}Sub");
    }

    [Fact] void ComplicatedExplicitId() {
        const String input =
            """
            # Altar Cave {#altar_cave}
            Paragraph.
            ## Enemies {#enemies}
            ### Level 1
            * [Carbuncle]()
            """;

        var result = Pipeline.RunFromMarkdown(input);
        result
            .FirstDescendant<Item>()
            .FirstDescendant<Item>()
            .FirstDescendant<Item>()
            .FirstDescendant<ListItem>().ImplicitId.Should().Be(
                $"enemies" +
                $"{ImplicitIdProcessor.GroupSep}Level{ImplicitIdProcessor.WordSep}1" +
                $"{ImplicitIdProcessor.GroupSep}Carbuncle"
            );
    }

    [Fact] void ComplicatedImplicitId() {
        const String input =
            """
            # The Floating Continent
            Paragraph.
            ## Mainland
            ### Northeast
            Paragraph
            * [Killer Bee]
            """;

        var result = Pipeline.RunFromMarkdown(input);
        result
            .FirstDescendant<ListItem>().ImplicitId.Should().Be(
                $"The{ImplicitIdProcessor.WordSep}Floating{ImplicitIdProcessor.WordSep}Continent" +
                $"{ImplicitIdProcessor.GroupSep}Mainland" +
                $"{ImplicitIdProcessor.GroupSep}Northeast" +
                $"{ImplicitIdProcessor.GroupSep}Killer{ImplicitIdProcessor.WordSep}Bee"
            );
    }

    [Fact] void Duplicates() {
        const String input =
            """
            # Heading One
            * Item
            * Item
            * Item
            """;
        Pipeline.RunFromMarkdown(input)
            .FirstDescendant<Listing>()
            .Children[2] // 3rd item
            .GlobalId.Should()
            .Be(
                $"Heading{ImplicitIdProcessor.WordSep}One" +
                $"{ImplicitIdProcessor.GroupSep}Item{ImplicitIdProcessor.RecordSep}3"
            );
    }

    [Fact] void BasicId() {
        const String input =
            """
            # Heading One
            * Item one
              * Item two
            """;
        var doc = Pipeline.RunFromMarkdown(input);
        doc
            .FirstDescendant<ListItem>()
            .FirstDescendant<ListItem>().GlobalId.Should().Be(
                $"Heading{ImplicitIdProcessor.WordSep}One" +
                $"{ImplicitIdProcessor.GroupSep}Item{ImplicitIdProcessor.WordSep}one" +
                $"{ImplicitIdProcessor.GroupSep}Item{ImplicitIdProcessor.WordSep}two"
            );
    }
}