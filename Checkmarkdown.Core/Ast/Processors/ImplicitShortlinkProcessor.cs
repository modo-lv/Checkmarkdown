using Checkmarkdown.Core.Elements;
using Checkmarkdown.Core.Elements.Meta;
using Checkmarkdown.Core.Project;

namespace Checkmarkdown.Core.Ast.Processors;

/// <summary>
/// Rewrites implicit shortlinks (<c>[a](@))</c>, <c>[a](#)</c> and just <c>[a]</c>) into regular ones.
/// </summary>
public class ImplicitShortlinkProcessor(CoreBuildContext buildContext) : AstProcessor(buildContext)
{
    public override Element Process(Element node) {
        if (node is Link { Target: "" } link) {
            var id = Globals.Id(node.TitleText);
            link.Target = buildContext.IdIndex.ContainsKey(id) ? $"#{id}" : $"@{node.TitleText}";
        }
        return node;
    }
}