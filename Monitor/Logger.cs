using Serilog;

namespace Monitor
{
    public static class Logger
    {
        public static Serilog.Core.Logger log = new LoggerConfiguration()
            .WriteTo.RollingFile("log.txt")
            .CreateLogger();
    }
}