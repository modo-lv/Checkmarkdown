using CommandLine;

Parser.Default.ParseArguments<Options>(args).WithParsed(opts => {
    Console.WriteLine($"Path: {opts.ProjectPath}");
});

public class Options
{
    [Value(index: 0, HelpText = "Path to the project directory.", Default = ".")]
    public String ProjectPath { get; set; } = ".";
}