using Checkmarkdown.Core.Ast;
using Checkmarkdown.Core.Ast.Processors;
using Checkmarkdown.Core.Elements;
using Checkmarkdown.Core.Project;
using Checkmarkdown.Core.Tests.Utils;
using Checkmarkdown.Core.Tests.Wiring;
using Checkmarkdown.Core.Utils;
using FluentAssertions;
using Xunit;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Checkmarkdown.Core.Tests.Checkmarkdown;

public class IdIndexTests : TestServices
{
    private IdIndex Index =>
        TestScope.Service<CoreBuildContext>().IdIndex;

    private AstProcessorPipeline Pipeline =>
        TestScope.Service<AstProcessorPipeline>()
            .Add(TestScope.Service<ExplicitIdProcessor>())
            .Add(TestScope.Service<IdIndexProcessor>());

    [Fact]
    void MultipleDocuments() {
        Pipeline.Also(it => {
            it.RunFromMarkdown(@"Text {#text}");
            it.RunFromMarkdown(@"[Link] {#link}");
        });
        Index.Should().ContainKeys("text", "link");
    }

    [Fact]
    void Implicit() {
        const String input = @"
## [Class Anchor](xxx) {#:id}
## **ID Anchor** {#:id}
";
        var doc = Pipeline.RunFromMarkdown(input);
        Index.Count.Should().Be(2);
        Index["class_anchor"].Should().Be(doc);
        Index["id_anchor"].Should().Be(doc);
    }

    [Fact]
    void Explicit() {
        const String input = @"
# Heading {#h-Anchor}
Paragraph {#pAnchor}
";
        var doc = Pipeline.RunFromMarkdown(input);
        doc.FirstDescendant<Heading>().ExplicitId.Should().Be("h-Anchor");
        doc.FirstDescendant<Paragraph>().ExplicitId.Should().Be("pAnchor");
        var anchors = Index;

        anchors.Count.Should().Be(2);
        anchors["h-Anchor"].Should().Be(doc);
        anchors["pAnchor"].Should().Be(doc);
    }

}