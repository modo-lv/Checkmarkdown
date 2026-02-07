using Checkmarkdown.Core.Ast;
using Checkmarkdown.Core.Ast.Processors;
using Checkmarkdown.Core.Elements;
using Checkmarkdown.Core.Project;
using Checkmarkdown.Core.Tests.Utils;
using Checkmarkdown.Core.Tests.Wiring;
using FluentAssertions;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Checkmarkdown.Core.Tests.Checkmarkdown;

public class ImplicitShortlinkTests : TestBuildContext
{
    static readonly AstProcessorPipeline _pipeline = new AstProcessorPipeline()
        .Add(new ImplicitShortlinkProcessor())
        .Add(new IdIndexProcessor());

    [Fact] void Internal() {
        var doc = _pipeline.RunFromMarkdown(
            """
            Target {#internal}
            [Internal]
            """
        );
        doc.FirstDescendant<Link>().Target.Should().Be(":internal");
    }

    [Fact] void External() {
        var doc = _pipeline.RunFromMarkdown("[External]");
        doc.FirstDescendant<Link>().Target.Should().Be("@External");
    }
}