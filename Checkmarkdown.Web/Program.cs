using System.Diagnostics;
using AngleSharp.Html;
using AngleSharp.Html.Parser;
using Checkmarkdown.Core;
using Checkmarkdown.Core.Utils;
using Checkmarkdown.Web;
using Checkmarkdown.Web.Ast;
using Checkmarkdown.Web.Project;
using CommandLine;
using MoreLinq;
using Serilog;

Parser.Default.ParseArguments<Options>(args).WithParsed(opts => {
    LogUtils.EnableLogging();
    Log.Information("Loading and building Checkmarkdown Web project...");
    var project = new WebProject(opts.ProjectPath);
    project.Load();
    var documents = project.FindPages().Let(pages => project.BuildDocuments(pages, WebAst.Pipeline));
    documents.ForEach(doc => {
        var htmlFile = doc.SourceFile!.Relative.ToString().TrimSuffix(".md") + ".html";
        var outFile = project.PathTo("out-web", htmlFile);
        outFile.Full.Parent().CreateDirectory();
        Log.Information("Building web output: {outFile}", outFile);
        var html = RazorHtmlBuilder.Build(doc);
        using var sw = new StringWriter();
        new HtmlParser().ParseDocument(html).ToHtml(sw, new PrettyMarkupFormatter {
            Indentation = "  "
        });
        Build.FileSystem.File.WriteAllText(outFile.FullPath, sw.ToString());
    });
    if (opts.Open != null) {
        var path = project.PathTo("out-web", "pages", opts.Open + ".html");
        if (!File.Exists(path.FullPath))
            Log.Error("Can't open file because it doesn't exist: {path}", path);
        else {
            Log.Information("Auto-opening: {path}", path);
            new Process().Also(proc => {
                proc.StartInfo.UseShellExecute = true;
                proc.StartInfo.FileName = path.FullPath;
                proc.Start();
            });
        }
    }
    Log.Information("All done.");
});

public class Options
{
    [Value(index: 0, HelpText = "Path to the project directory.", Default = ".")]
    public String ProjectPath { get; set; } = ".";

    [Option(HelpText =
        """
        Automatically open this page in the default browser once the project has been built.
        Page path relative to "pages" folder, without extension: "index", "chapters/1".
        """
    )]
    public String? Open {
        get;
        init => field = value?.Trim().TakeUnless(it => it.IsWhiteSpace());
    } = null;
}