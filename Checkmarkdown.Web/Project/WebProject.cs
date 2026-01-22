using Checkmarkdown.Core;
using Checkmarkdown.Core.Project;
using Checkmarkdown.Core.Utils;
using Checkmarkdown.Web.Project.Config;
using Newtonsoft.Json;
using Serilog;

namespace Checkmarkdown.Web.Project;

public class WebProject(String rootPath) : ProjectBase(rootPath)
{
    public WebConfig Config {
        get => field.NotNull(orError: $"Can't access [{nameof(WebProject)}.{nameof(Config)}], not loaded.");
        set;
    }

    public void Load() {
        var configFile = this.PathTo("checkmarkdown-web-config.json");

        if (!configFile.Exists) {
            Log.Information(
                "Configuration file {file} not found, creating with default values...", configFile
            );
            Config = new WebConfig(
                ProjectId: Guid.CreateVersion7()
            );

            var json = JsonConvert.SerializeObject(Config, Formatting.Indented);
            Build.FileSystem.File.WriteAllText(path: $"{configFile.Full}", contents: json);
            Log.Debug("Wrote {Json} to {FullPathString}", json, $"{configFile.Full}");
        } else {
            var json = Build.FileSystem.File.ReadAllText(path: $"{configFile.Full}");
            Config = JsonConvert.DeserializeObject<WebConfig>(json)
                     ?? throw new InvalidDataException("Config file exists, but contains no config data.");
        }

        Log.Information("Web project loaded, ID: {id}", Config.ProjectId);
    }
}