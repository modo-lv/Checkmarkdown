using Checkmarkdown.Core.Utils;
using Checkmarkdown.Web.Project;
using CommandLine;
using Path = Fluent.IO.Path;

Parser.Default.ParseArguments<Options>(args).WithParsed(opts => {
    Console.WriteLine($"Path: {opts.ProjectPath}");
    new WebProject(new Path(opts.ProjectPath)).Also(it => {
        it.Load();
    });
});

public class Options
{
    [Value(index: 0, HelpText = "Path to the project directory.", Default = ".")]
    public String ProjectPath { get; set; } = ".";
}