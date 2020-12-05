using System;
using Serilog;

namespace KBot.Common.Logging
{
    public sealed class Logger
    {
        private readonly ILogger logger;

        public Logger()
        {
            logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
        
        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void Information(string message)
        {
            logger.Information(message);
        }

        public void Warning(string message)
        {
            logger.Warning(message);
        }

        public void Error(string message, Exception exception)
        {
            logger.Error(exception, message);
        }
    }
}