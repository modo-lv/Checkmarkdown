using Checkmarkdown.Core;
using Checkmarkdown.Core.Utils;
using Checkmarkdown.Web;
using Checkmarkdown.Web.Project;
using CommandLine;
using MoreLinq;
using Serilog;

Parser.Default.ParseArguments<Options>(args).WithParsed(opts => {
    LogUtils.EnableLogging();
    Log.Information("Loading and building Checkmarkdown Web project...");
    new WebProject(opts.ProjectPath).Also(project => {
        project.Load();
        var documents = project.FindPages().Let(project.BuildDocuments);
        documents.ForEach(doc => {
            var htmlFile = doc.SourceFile!.Relative.ToString().TrimSuffix(".md") + ".html";
            var outFile = project.PathTo("out-web", htmlFile);
            outFile.Full.Parent().CreateDirectory();
            Log.Information("Building web output: {outFile}", outFile);
            var html = RazorHtmlBuilder.Build(doc);
            Build.FileSystem.File.WriteAllText(outFile.FullPath, html);
        });
    });
    Log.Information("All done.");
});

public class Options
{
    [Value(index: 0, HelpText = "Path to the project directory.", Default = ".")]
    public String ProjectPath { get; set; } = ".";
}