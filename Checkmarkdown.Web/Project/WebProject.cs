using Checkmarkdown.Core;
using Checkmarkdown.Core.Project;
using Checkmarkdown.Web.Project.Config;
using Newtonsoft.Json;
using Serilog;
using Path = Fluent.IO.Path;

namespace Checkmarkdown.Web.Project;

public class WebProject(Path rootPath) : ProjectBase(rootPath)
{
    public void Load() {
        var configFile = this.PathTo("checkmarkdown-web-config.json");

        if (!configFile.Exists) {
            var defaultConfig = new WebConfig(
                ProjectId: Guid.CreateVersion7()
            );

            var json = JsonConvert.SerializeObject(defaultConfig, Formatting.Indented);
            App.FileSystem.File.WriteAllText(path: configFile.FullPathString(), contents: json);

            Log.Information("Wrote {Json} to {FullPathString}", json, configFile.FullPathString());
        }

    }
}