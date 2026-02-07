using Checkmarkdown.Core.Elements.Meta;

namespace Checkmarkdown.Core.Ast.Processors;

/// <summary>
/// Normalizes explicit IDs and enables title-based generation.
/// </summary>
/// <remarks>
/// Markdig understands <c>{#id}</c> syntax and we set the ID from that during <see cref="FromMarkdown"/>
/// conversion, but we also support generating the ID from title with <c>{#:id}</c>.
/// </remarks>
public class ExplicitIdProcessor : AstProcessor
{
    public override Element Process(Element node) {
        if (node.ExplicitId is { } id)
            node.ExplicitId = Globals.Id(id.Replace(":id", node.TitleText));
        return node;
    }
}