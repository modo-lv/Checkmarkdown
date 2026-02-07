using Checkmarkdown.Core.Project;

namespace Checkmarkdown.Core.Tests.Wiring;

// ReSharper disable once ClassNeverInstantiated.Global
public class TestBuildContext : IDisposable
{
    public TestBuildContext() {
        Build.Context = new ProjectBuildContext();
    }

    public void Dispose() { }
}