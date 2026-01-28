using Checkmarkdown.Core.Elements;
using Checkmarkdown.Web.Project;
using RazorLight;

namespace Checkmarkdown.Web;

public class HtmlBuilder
{
    private static RazorLightEngine _engine = new RazorLightEngineBuilder()
        .UseEmbeddedResourcesProject(typeof(WebProject))
        .UseMemoryCachingProvider()
        .Build();

    public static String Build(Document document) {
        var templatePath = Path.Combine("Resources", "cshtml", "page.cshtml");
        var templateContent = File.ReadAllText(templatePath);
        var result = _engine.CompileRenderStringAsync("page", templateContent, document).Result!;
        return result;
    }
}