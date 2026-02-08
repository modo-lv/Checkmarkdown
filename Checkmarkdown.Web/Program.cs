using System.Diagnostics;
using AngleSharp.Html;
using AngleSharp.Html.Parser;
using Checkmarkdown.Core;
using Checkmarkdown.Core.Ast;
using Checkmarkdown.Core.Project;
using Checkmarkdown.Core.Utils;
using Checkmarkdown.Core.Wiring;
using Checkmarkdown.Web;
using Checkmarkdown.Web.Ast;
using Checkmarkdown.Web.Builders;
using Checkmarkdown.Web.Project;
using Checkmarkdown.Web.Utils;
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

    AstProcessorPipeline.RunOrder.Add(typeof(ShortlinkProcessor));

    // Configure services, crucially the build context and extra AST processors
    var services = new ServiceCollection()
        .Let(CoreServices.Configure)
        .AddScoped<WebBuildContext>(_ => new WebBuildContext(project.Config))
        .RemoveAll<CoreBuildContext>()
        .AddScoped<CoreBuildContext, WebBuildContext>(sp => sp.GetRequiredService<WebBuildContext>())
        .AddScoped<ShortlinkProcessor>()
        .BuildServiceProvider();
    using var scope = services.CreateScope();

    // Build the AST
    var documents = project.FindPages().Let(pages =>
        // ReSharper disable once AccessToDisposedClosure
        project.BuildDocuments(pages, scope.FullWebAstPipeline())
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
    
    // CSS
    CssBuilder.BuildCss(project);

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