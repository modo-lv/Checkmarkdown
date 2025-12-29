using Checkmarkdown.Core.Ast;
using Checkmarkdown.Core.Ast.Processors;
using Checkmarkdown.Core.Elements;
using Checkmarkdown.Core.Tests.Utils;
using FluentAssertions;
using Xunit;

namespace Checkmarkdown.Core.Tests.Checkmarkdown;

public class ListItemTests {
    [Fact] void FirstChildAttributesMoveToItem() {
        const String input = "* Item {:attr}";
        var doc = new AstProcessorPipeline().Add(new ListItemAttributeProcessor()).Run(input);
        doc.FirstDescendant<ListItem>().Attributes.Flag("attr").Should().BeTrue();
    }
}