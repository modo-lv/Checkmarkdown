using Checkmarkdown.Core.Utils;
using Checkmarkdown.Core.Wiring;
using Microsoft.Extensions.DependencyInjection;

namespace Checkmarkdown.Core.Tests.Wiring;

public class TestServices : IDisposable
{
    protected readonly IServiceScope TestScope = 
        new ServiceCollection()
            .Let(CoreServices.Configure)
            .BuildServiceProvider()
            .CreateScope();

    public void Dispose() => TestScope.Dispose();
}