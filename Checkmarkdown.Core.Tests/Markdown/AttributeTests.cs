using Checkmarkdown.Core.Ast;
using Checkmarkdown.Core.Elements;
using Checkmarkdown.Core.Tests.Utils;
using FluentAssertions;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Checkmarkdown.Core.Tests.Markdown; 

public class AttributesTests {
  [Fact] void BasicCase() {
    const String input = "*xxx* {:setting}";
    var result = FromMarkdown.ToCheckmarkdown(input);
    result.FirstDescendant<Paragraph>().Attributes.Flag("setting").Should().BeTrue();
  }
}