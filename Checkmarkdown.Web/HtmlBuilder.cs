using Checkmarkdown.Core.Elements;
using Checkmarkdown.Web.Project;
using RazorLight;

namespace Checkmarkdown.Web;

public class HtmlBuilder {
    public String Build(Document document) {
        var templatePath = Path.Combine("Resources", "cshtml", "page.cshtml");
        var templateContent = File.ReadAllText(templatePath);
        var engine = new RazorLightEngineBuilder()
            .UseEmbeddedResourcesProject(typeof(WebProject))
            .UseMemoryCachingProvider()
            .Build();
        var result = engine.CompileRenderStringAsync("key", templateContent, document).Result!;
        return result;
    }
}