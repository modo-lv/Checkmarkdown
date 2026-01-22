using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace Checkmarkdown.Core.Utils;

public class LogUtils
{
    /// <summary>Configures Serilog's <see cref="Log"/> to output to console.</summary>
    public static void EnableLogging() {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console(theme: AnsiConsoleTheme.Code)
            .MinimumLevel.Is(LogEventLevel.Debug) // TODO: Make configurable and Info by default
            .CreateLogger();
    }
}