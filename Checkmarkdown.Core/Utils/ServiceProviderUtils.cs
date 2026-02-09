using Checkmarkdown.Core.Ast;
using Checkmarkdown.Core.Ast.Processors;
using Microsoft.Extensions.DependencyInjection;

namespace Checkmarkdown.Core.Utils;

public static class ServiceProviderUtils
{
    extension(IServiceScope scope)
    {
        public T Service<T>() where T : class {
            return scope.ServiceProvider.GetRequiredService<T>();
        }

        /// <summary>Returns an AST pipeline containing all core processors.</summary>
        public AstProcessorPipeline FullCoreAstPipeline() {
            return scope.Service<AstProcessorPipeline>()
                .Add(scope.Service<DocumentAttributeProcessor>())
                .Add(scope.Service<ExplicitIdProcessor>())
                .Add(scope.Service<HeadingItemProcessor>())
                .Add(scope.Service<IdIndexProcessor>())
                .Add(scope.Service<ImplicitIdProcessor>())
                .Add(scope.Service<ImplicitShortlinkProcessor>())
                .Add(scope.Service<ListItemAttributeProcessor>())
                .Add(scope.Service<ItemListUnwrapProcessor>());
        }
    }
}