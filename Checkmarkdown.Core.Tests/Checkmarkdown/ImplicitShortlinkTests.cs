using Checkmarkdown.Core.Ast;
using Checkmarkdown.Core.Ast.Processors;
using Checkmarkdown.Core.Elements;
using Checkmarkdown.Core.Tests.Utils;
using Checkmarkdown.Core.Tests.Wiring;
using Checkmarkdown.Core.Utils;
using FluentAssertions;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Checkmarkdown.Core.Tests.Checkmarkdown;

public class ImplicitShortlinkTests : TestServices
{
    private AstProcessorPipeline Pipeline =>
        TestScope.Service<AstProcessorPipeline>()
            .Add(TestScope.Service<ImplicitShortlinkProcessor>())
            .Add(TestScope.Service<IdIndexProcessor>());


    [Fact] void Internal() {
        var doc = Pipeline.RunFromMarkdown(
            """
            Target {#internal}
            [Internal]
            """
        );
        doc.FirstDescendant<Link>().Target.Should().Be("#internal");
    }

    [Fact] void External() {
        var doc = Pipeline.RunFromMarkdown("[External]");
        doc.FirstDescendant<Link>().Target.Should().Be("@External");
    }
}