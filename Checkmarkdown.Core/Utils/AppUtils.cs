using Serilog;

namespace Checkmarkdown.Core.Utils;

public static class AppUtils
{
    private static readonly TimeSpan _exitDelay = TimeSpan.FromSeconds(5);
    /**
     * Log a fatal error message and exit the application.
     */
    public static void FatalExit(String message, params Object[] vars) {
#pragma warning disable CA2254 // Template should be a static expression
        Log.Fatal(message, vars);
#pragma warning restore CA2254 // Template should be a static expression
        Console.WriteLine(
            $"Fatal error encountered (see above), exiting in {_exitDelay.Seconds} second(s)..."
        );
        Thread.Sleep(_exitDelay);
        Environment.Exit(1);
    }
}