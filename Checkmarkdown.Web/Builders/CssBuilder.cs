using Checkmarkdown.Core;
using Checkmarkdown.Web.Project;
using MoreLinq;
using Serilog;
using Path = Fluent.IO.Path;

namespace Checkmarkdown.Web.Builders;

public static class CssBuilder
{
    public static void BuildCss(WebProject project) {
        // Since we're using plain CSS, all we have to do is just copy the files.
        var sourceDir = new Path(AppContext.BaseDirectory).Combine("Resources", "css")!;
        var targetDir = project.PathTo("out-web", "css");
        Build.FileSystem.Directory.CreateDirectory(targetDir.FullPath);
        Log.Information("Copying CSS files: {in} to {out}", sourceDir.FullPath, targetDir);
        sourceDir.Files("*.css", recursive: true).ForEach(sourceFile => {
            var targetFile = targetDir.Full.Combine(sourceFile.FileName)!;

            if (!targetFile.Exists || sourceFile.LastWriteTime() > targetFile.LastWriteTime()) {
                Build.FileSystem.File.Copy(
                    sourceFile.FullPath,
                    targetFile.FullPath,
                    overwrite: true
                );
                Log.Debug("CSS file copied: {path}", targetFile.FullPath);
            } else {
                Log.Debug("CSS file up to date, skipping: {path}", targetFile.FullPath);
            }
        });
    }
}