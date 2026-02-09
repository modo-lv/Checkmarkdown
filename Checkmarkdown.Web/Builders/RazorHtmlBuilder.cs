using Checkmarkdown.Core.Elements;
using RazorLight;
using Path = Fluent.IO.Path;

namespace Checkmarkdown.Web.Builders;

public static class RazorHtmlBuilder
{
    static RazorHtmlBuilder() {
        var root = $"{new Path(Environment.ProcessPath).Parent().Combine("Resources", "cshtml")}";
        _engine = new RazorLightEngineBuilder()
            .UseFileSystemProject(root)
            .UseMemoryCachingProvider()
            .Build();
    }

    private static readonly RazorLightEngine _engine;

    public static String Build(Document document) {
        var templatePath = System.IO.Path.Combine("Resources", "cshtml", "page.cshtml");
        var templateContent = File.ReadAllText(templatePath);
        RazorHtmlHelper.Document = document;
        var result = _engine.CompileRenderStringAsync("page", templateContent, document).Result!;
        return result;
    }
}