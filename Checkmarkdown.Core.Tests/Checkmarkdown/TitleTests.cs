using Checkmarkdown.Core.Ast;
using Checkmarkdown.Core.Elements;
using Checkmarkdown.Core.Tests.Utils;
using Checkmarkdown.Core.Tests.Wiring;
using Checkmarkdown.Core.Utils;
using FluentAssertions;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Checkmarkdown.Core.Tests.Checkmarkdown;

public class TitleTests : TestServices
{
    private AstProcessorPipeline Pipeline => TestScope.Service<AstProcessorPipeline>();

    [Fact] void HeadingWithLinkTitle() {
        const String input = "# Heading with [link](link)";
        var result = Pipeline.RunFromMarkdown(input);
        result.FirstDescendant<Heading>().TitleText.Should().Be("Heading with link");
    }

    [Fact] void ListItemTitle() {
        const String input =
            """
            * **[Wicked](link)**
              Continuation

              Another paragraph
            """;
        var result = Pipeline.RunFromMarkdown(input);
        result.Children[0].TitleText.Should().Be("Wicked");
    }

}