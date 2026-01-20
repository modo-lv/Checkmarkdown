using Checkmarkdown.Core.Utils;
using Checkmarkdown.Web.Project;
using CommandLine;
using Serilog;
using Path = Fluent.IO.Path;

Parser.Default.ParseArguments<Options>(args).WithParsed(opts => {
    LogUtils.EnableLogging();
    Log.Information("Path: {ProjectPath}", opts.ProjectPath);
    new WebProject(new Path(opts.ProjectPath)).Also(project => {
        project.Load();
    });
});

public class Options
{
    [Value(index: 0, HelpText = "Path to the project directory.", Default = ".")]
    public String ProjectPath { get; set; } = ".";
}