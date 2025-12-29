using Checkmarkdown.Core.Markdown.Extensions;
using Checkmarkdown.Core.Utils;
using Markdig;
using Markdig.Extensions.CustomContainers;
using Markdig.Extensions.Tables;
using Markdig.Syntax;

namespace Checkmarkdown.Core.Markdown;

public static class MarkdownParser {
    private static readonly Lazy<MarkdownPipeline> _pipeline = new Lazy<MarkdownPipeline>(() =>
        new MarkdownPipelineBuilder().Also(it => {
            it.Extensions.AddIfNotAlready(new CustomContainerExtension());
            it.Extensions.AddIfNotAlready(new PipeTableExtension());
            it.UseGenericAttributes();
        }).Build()
    );

    public static MarkdownDocument ParseToAst(String markdown) {
        return Markdig.Markdown.Parse(markdown, _pipeline.Value);
    }
}