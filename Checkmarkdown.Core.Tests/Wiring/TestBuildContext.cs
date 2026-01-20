using System;

namespace Checkmarkdown.Core.Tests.Wiring;

// ReSharper disable once ClassNeverInstantiated.Global
public class TestBuildContext : IDisposable {
    
    public TestBuildContext() {
        Build.Context = new();
    }
    
    public void Dispose() { }
    
}
