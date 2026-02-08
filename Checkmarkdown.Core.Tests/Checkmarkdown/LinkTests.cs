using Checkmarkdown.Core.Ast;
using Checkmarkdown.Core.Elements;
using Checkmarkdown.Core.Tests.Utils;
using Checkmarkdown.Core.Tests.Wiring;
using FluentAssertions;
using Xunit;

namespace Checkmarkdown.Core.Tests.Checkmarkdown;

public class LinkTests : TestServices
{
    [Fact] void ExplicitExternalLink() {
        const String input = "[page](http://example.com)";
        var result = FromMarkdown.ToCheckmarkdown(input, null);
        result.FirstDescendant<Link>().Target.Should().Be("http://example.com");
    }
}