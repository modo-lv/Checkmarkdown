using Checkmarkdown.Core.Ast;
using Checkmarkdown.Core.Utils;
using Checkmarkdown.Web.Ast;
using Microsoft.Extensions.DependencyInjection;

namespace Checkmarkdown.Web.Utils;

public static class ServiceProviderUtils
{
    extension(IServiceScope scope)
    {
        /// <summary>Returns an AST pipeline containing all core processors.</summary>
        public AstProcessorPipeline FullWebAstPipeline() {
            return scope.FullCoreAstPipeline()
                .Add(scope.Service<ShortlinkProcessor>());
        }
    }
}