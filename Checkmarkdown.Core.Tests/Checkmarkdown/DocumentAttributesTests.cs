using Checkmarkdown.Core.Ast;
using Checkmarkdown.Core.Ast.Processors;
using Checkmarkdown.Core.Elements;
using FluentAssertions;
using Xunit;
// ReSharper disable ArrangeTypeMemberModifiers

namespace Checkmarkdown.Core.Tests.Checkmarkdown; 

public class DocumentAttributesTests {
  [Fact] void SimpleCase() {
    const String input = ":{#id .class property :setting}";
    var result = new AstProcessorPipeline().Add(new DocumentAttributeProcessor()).Run(input);
    var doc = result.As<Document>();
    doc.Children.Should().BeEmpty();
    doc.ExplicitId.Should().Be("id");
    doc.Attributes.Classes.Should().Contain("class");
    doc.Attributes.Properties.Should().ContainKey("property");
    doc.Attributes.Flag("setting").Should().BeTrue();
  }
}