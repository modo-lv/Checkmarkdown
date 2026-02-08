using Checkmarkdown.Core.Ast;
using Checkmarkdown.Core.Ast.Processors;
using Checkmarkdown.Core.Elements;
using Checkmarkdown.Core.Tests.Utils;
using Checkmarkdown.Core.Tests.Wiring;
using Checkmarkdown.Core.Utils;
using FluentAssertions;
using Xunit;

namespace Checkmarkdown.Core.Tests.Checkmarkdown;

public class ListItemTests : TestServices
{
    [Fact] void FirstChildAttributesMoveToItem() {
        const String input = "* Item {:attr}";
        var doc = TestScope.Service<AstProcessorPipeline>()
            .Add(TestScope.Service<ListItemAttributeProcessor>())
            .RunFromMarkdown(input);
        doc.FirstDescendant<ListItem>().Attributes.Flag("attr").Should().BeTrue();
    }
}