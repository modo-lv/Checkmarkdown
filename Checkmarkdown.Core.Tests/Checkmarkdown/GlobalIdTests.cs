using Checkmarkdown.Core.Ast;
using Checkmarkdown.Core.Ast.Processors;
using Checkmarkdown.Core.Elements;
using Checkmarkdown.Core.Tests.Utils;
using Checkmarkdown.Core.Tests.Wiring;
using Checkmarkdown.Core.Wiring.Errors;
using FluentAssertions;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Checkmarkdown.Core.Tests.Checkmarkdown;

public class GlobalIdTests : IClassFixture<TestBuildContext> {

    [Fact] void Basic() {
        const String input = "a {#:id}";
        var result = new AstProcessorPipeline().Add(new TitleIdProcessor()).RunFromMarkdown(input);
        result.FirstDescendant<Paragraph>().ExplicitId.Should().Be("a");
    }

    [Fact] void ItemTree() {
        const String input =
            """
            # Heading One
            * Item
              * X {#xx}
            """;

        var result = new AstProcessorPipeline()
            .Add(new ListItemAttributeProcessor())
            .Add(new TitleIdProcessor())
            .RunFromMarkdown(input);
        result
            .FirstDescendant<ListItem>()
            .FirstDescendant<ListItem>().ExplicitId.Should().Be("xx");
    }

    // TODO: Add a test that tests this across multiple documents
    [Fact] void ThrowOnDuplicate() {
        const String input = @"Text {#id}

More text {#id}";
        FluentActions.Invoking(() =>
            AstProcessorPipeline.CreateDefault().RunFromMarkdown(input)
        ).Should().Throw<DuplicateIdException>();
    }

}