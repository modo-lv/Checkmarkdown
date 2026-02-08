using Checkmarkdown.Core.Ast;

namespace Checkmarkdown.Web.Ast;

public static class WebAst
{
    public static readonly AstProcessorPipeline Pipeline;

    static WebAst() {
        Pipeline = AstProcessorPipeline.CreateDefault();
    }
}