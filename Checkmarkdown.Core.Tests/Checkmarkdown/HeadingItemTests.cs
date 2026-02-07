using Checkmarkdown.Core.Ast;
using Checkmarkdown.Core.Ast.Processors;
using Checkmarkdown.Core.Elements;
using Checkmarkdown.Core.Tests.Utils;
using Checkmarkdown.Core.Utils;
using FluentAssertions;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Checkmarkdown.Core.Tests.Checkmarkdown;

public class HeadingItemTests {

    private static AstProcessorPipeline _pipeline = 
        new AstProcessorPipeline().Add(new HeadingItemProcessor());

    [Fact] void InnerItems() {
        const String input =
            """
            # Top
            ## Mid
            ### Bot
            """;
        var result = _pipeline.RunFromMarkdown(input);
        result
            .FirstDescendant<Item>().Also(it => {
                it.IsHeading.Should().BeTrue();
                it.Title!.TitleText.Should().Be("Top");
            })
            .FirstDescendant<Item>().Also(it => {
                it.IsHeading.Should().BeTrue();
                it.Title!.TitleText.Should().Be("Mid");
            })
            .FirstDescendant<Item>().Also(it => {
                it.IsHeading.Should().BeTrue();
                it.Title!.TitleText.Should().Be("Bot");
            });
    }
    
    [Fact] void ListHeadingsAreSkipped() {
        const String input = "+ # Heading";
        var result = _pipeline.RunFromMarkdown(input);
        result
            .FirstDescendant<ListItem>()
            .Children[0].Should().NotBeOfType<Item>();
    }


}