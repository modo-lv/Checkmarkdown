using Checkmarkdown.Core.Ast;
using Checkmarkdown.Core.Ast.Processors;
using Checkmarkdown.Core.Elements;
using Checkmarkdown.Core.Tests.Utils;
using FluentAssertions;
using Xunit;
// ReSharper disable ArrangeTypeMemberModifiers

namespace Checkmarkdown.Core.Tests.Checkmarkdown;

public class TitleIdTests {

    [Fact] void Basic() {
        const String input = "a {#:id}";
        var result = new AstProcessorPipeline().Add(new TitleIdProcessor()).Run(input);
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
            .Run(input);
        result
            .FirstDescendant<ListItem>()
            .FirstDescendant<ListItem>().ExplicitId.Should().Be("xx");
    }
}