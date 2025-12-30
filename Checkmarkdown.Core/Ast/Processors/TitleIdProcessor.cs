using Checkmarkdown.Core.Elements.Meta;

namespace Checkmarkdown.Core.Ast.Processors;

/// <summary>
/// Markdig understands <c>{#id}</c> syntax and we set the ID from that if present,
/// but we also want to support generating the ID from title with <c>{#:id}</c>.
/// </summary>
public class TitleIdProcessor : AstProcessor {
    public override Element Process(Element node) {
        if (node.ExplicitId == ":id")
            node.ExplicitId = Globals.Id(node.TitleText);
        return node;
    }
}