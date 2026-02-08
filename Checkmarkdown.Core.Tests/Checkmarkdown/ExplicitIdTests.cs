using Checkmarkdown.Core.Ast;
using Checkmarkdown.Core.Ast.Processors;
using Checkmarkdown.Core.Elements;
using Checkmarkdown.Core.Tests.Utils;
using Checkmarkdown.Core.Tests.Wiring;
using Checkmarkdown.Core.Utils;
using Checkmarkdown.Core.Wiring.Errors;
using FluentAssertions;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Checkmarkdown.Core.Tests.Checkmarkdown;

public class ExplicitIdTests : TestServices
{

    [Fact] void Basic() {
        const String input = "a {#:id}";
        var result = TestScope.Service<AstProcessorPipeline>()
            .Add(TestScope.Service<ExplicitIdProcessor>())
            .RunFromMarkdown(input);
        result.FirstDescendant<Paragraph>().ExplicitId.Should().Be("a");
    }

    [Fact] void ItemTree() {
        const String input =
            """
            # Heading One
            * Item
              * X {#xx}
            """;

        var result =
            TestScope.Service<AstProcessorPipeline>()
                .Add(TestScope.Service<ListItemAttributeProcessor>())
                .Add(TestScope.Service<ExplicitIdProcessor>())
                .RunFromMarkdown(input);
        result
            .FirstDescendant<ListItem>()
            .FirstDescendant<ListItem>().ExplicitId.Should().Be("xx");
    }

    [Fact] void ThrowOnDuplicate() {
        const String input = @"Text {#id}

More text {#id}";
        FluentActions.Invoking(() =>
            TestScope.FullCoreAstPipeline().RunFromMarkdown(input)
        ).Should().Throw<DuplicateIdException>();
    }

}