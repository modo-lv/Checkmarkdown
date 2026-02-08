using Checkmarkdown.Core.Ast;
using Checkmarkdown.Core.Ast.Processors;
using Checkmarkdown.Core.Project;
using Microsoft.Extensions.DependencyInjection;

namespace Checkmarkdown.Core.Wiring;

public static class CoreServices
{
    public static IServiceCollection Configure(IServiceCollection services) {
        services.AddScoped<CoreBuildContext>();
        services.AddScoped<AstProcessorPipeline>();
        // AST processors
        services.AddScoped<DocumentAttributeProcessor>();
        services.AddScoped<ExplicitIdProcessor>();
        services.AddScoped<HeadingItemProcessor>();
        services.AddScoped<IdIndexProcessor>();
        services.AddScoped<ImplicitIdProcessor>();
        services.AddScoped<ImplicitShortlinkProcessor>();
        services.AddScoped<ListItemAttributeProcessor>();
        return services;
    }
}