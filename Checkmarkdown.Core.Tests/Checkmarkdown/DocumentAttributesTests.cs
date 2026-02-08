using Checkmarkdown.Core.Ast;
using Checkmarkdown.Core.Ast.Processors;
using Checkmarkdown.Core.Elements;
using Checkmarkdown.Core.Tests.Utils;
using Checkmarkdown.Core.Tests.Wiring;
using Checkmarkdown.Core.Utils;
using Xunit;
using AssertionExtensions = FluentAssertions.AssertionExtensions;

// ReSharper disable ArrangeTypeMemberModifiers

namespace Checkmarkdown.Core.Tests.Checkmarkdown;

public class DocumentAttributesTests : TestServices
{
    [Fact] void SimpleCase() {
        const String input = ":{#id .class property :setting}";
        var result =
            TestScope.Service<AstProcessorPipeline>()
                .Add(TestScope.Service<DocumentAttributeProcessor>())
                .RunFromMarkdown(input);
        var doc = result.As<Document>();
        AssertionExtensions.Should(doc.TitleText).BeEmpty();
        AssertionExtensions.Should(doc.Children).BeEmpty();
        AssertionExtensions.Should(doc.ExplicitId).Be("id");
        AssertionExtensions.Should(doc.Attributes.Classes).Contain("class");
        AssertionExtensions.Should(doc.Attributes.Properties).ContainKey("property");
        AssertionExtensions.Should(doc.Attributes.Flag("setting")).BeTrue();
    }
}