using HandlebarsDotNet;

namespace Checkmarkdown.Web;

public class HtmlBuilder {
    public String Build() {
        var templatePath = Path.Combine("Resources", "html", "index.hbs");
        var templateContent = File.ReadAllText(templatePath);
        var template = Handlebars.Compile(templateContent);
        var data = new {
            person = new { name = "Alice" },
        };
        return template(data);
    }
}