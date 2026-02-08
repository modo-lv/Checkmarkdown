using Checkmarkdown.Core.Elements.Meta;
using Checkmarkdown.Core.Project;
using Checkmarkdown.Core.Utils;

namespace Checkmarkdown.Core.Ast.Processors;

/// <summary>
/// Normalizes explicit IDs and enables title-based generation.
/// </summary>
/// <remarks>
/// Markdig understands <c>{#id}</c> syntax and we set the ID from that during <see cref="FromMarkdown"/>
/// conversion, but we also support generating the ID from title with <c>{#:id}</c>.
/// </remarks>
public class ExplicitIdProcessor(ProjectBuildContext buildContext) : AstProcessor(buildContext)
{
    public override Element Process(Element node) {
        if (node.ExplicitId is { } id)
            node.ExplicitId = id.TakeUnless(":id".Equals) ?? Globals.Id(node.TitleText);
        return node;
    }
}