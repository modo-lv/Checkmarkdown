using Checkmarkdown.Core;
using Checkmarkdown.Web.Project;
using Serilog;
using Path = Fluent.IO.Path;

namespace Checkmarkdown.Web.Builders;

public record CopyAssetsBuilder(
    String Name,
    String Dir,
    String Mask
)
{
    public void CopyAssets(WebProject project) {
        var sourceDir = new Path(AppContext.BaseDirectory).Combine("Resources", Dir)!;
        var targetDir = project.PathTo("out-web", Dir);
        Build.FileSystem.Directory.CreateDirectory(targetDir.FullPath);
        Log.Information("Copying {name} files: {in} to {out}", Name, sourceDir.FullPath, targetDir);
        sourceDir.Files(Mask, recursive: true).ForEach(sourceFile => {
            var targetFile = targetDir.Full.Combine(sourceFile.FileName)!;
            if (!targetFile.Exists || sourceFile.LastWriteTime() > targetFile.LastWriteTime()) {
                Build.FileSystem.File.Copy(
                    sourceFile.FullPath,
                    targetFile.FullPath,
                    overwrite: true
                );
                Log.Debug("{name} file copied: {path}", Name, targetFile.FullPath);
            } else {
                Log.Debug("{name} file up to date, skipping: {path}", Name, targetFile.FullPath);
            }
        });
    }
}