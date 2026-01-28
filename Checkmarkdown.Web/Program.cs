using Checkmarkdown.Core.Utils;
using Checkmarkdown.Web.Project;
using CommandLine;
using Serilog;

Parser.Default.ParseArguments<Options>(args).WithParsed(opts => {
    LogUtils.EnableLogging();
    Log.Information("Loading and building Checkmarkdown Web project...");
    new WebProject(opts.ProjectPath).Also(project => {
        project.Load();
        project.FindPages().Let(project.BuildPages);
    });
});

public class Options
{
    [Value(index: 0, HelpText = "Path to the project directory.", Default = ".")]
    public String ProjectPath { get; set; } = ".";
}