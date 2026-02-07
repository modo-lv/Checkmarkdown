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

public class IdIndexTests : TestBuildContext
{
    private static IdIndex Index => Build.Context.IdIndex;
    private static readonly AstProcessorPipeline _pipeline = new AstProcessorPipeline()
        .Add(new ExplicitIdProcessor())
        .Add(new IdIndexProcessor());

    [Fact]
    void MultipleDocuments() {
        _pipeline.RunFromMarkdown(@"Text {#text}");
        _pipeline.RunFromMarkdown(@"[Link] {#link}");
        Index.Should().ContainKeys("text", "link");
    }

    [Fact]
    void Implicit() {
        const String input = @"
## [Class Anchor](xxx) {#:id}
## **ID Anchor** {#:id}
";
        var doc = _pipeline.RunFromMarkdown(input);
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
        var doc = _pipeline.RunFromMarkdown(input);
        doc.FirstDescendant<Heading>().ExplicitId.Should().Be("h-anchor");
        doc.FirstDescendant<Paragraph>().ExplicitId.Should().Be("panchor");
        var anchors = Index;

        anchors.Count.Should().Be(2);
        anchors["h-anchor"].Should().Be(doc);
        anchors["panchor"].Should().Be(doc);
    }

}