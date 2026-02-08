using System.Diagnostics;
using AngleSharp.Html;
using AngleSharp.Html.Parser;
using Checkmarkdown.Core;
using Checkmarkdown.Core.Project;
using Checkmarkdown.Core.Utils;
using Checkmarkdown.Core.Wiring;
using Checkmarkdown.Web;
using Checkmarkdown.Web.Project;
using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MoreLinq;
using Serilog;

Parser.Default.ParseArguments<Options>(args).WithParsed(opts => {
    LogUtils.EnableLogging();

    // Load the project
    Log.Information("Loading and building Checkmarkdown Web project...");
    var project = new WebProject();
    project.Load(opts.ProjectPath);

    // Configure services, crucially the build context and extra AST processors
    var services = new ServiceCollection()
        .Let(CoreServices.Configure)
        .RemoveAll<CoreBuildContext>()
        .AddScoped<CoreBuildContext, WebBuildContext>()
        .BuildServiceProvider();
    using var scope = services.CreateScope();

    // Build the AST
    var documents = project.FindPages().Let(pages => 
        // ReSharper disable once AccessToDisposedClosure
        project.BuildDocuments(pages, scope.FullCoreAstPipeline())
    );

    // Convert to output
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
    
    // Auto-open a result if configured
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