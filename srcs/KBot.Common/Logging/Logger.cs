using System;
using System.Reflection;
using Serilog;

namespace KBot.Common.Logging
{
    public sealed class Logger
    {
        private readonly ILogger logger;

        public Logger(string name)
        {
            logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File($"KBot/logs/{name}.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public void Trace(string message)
        {
            logger.Verbose(message);
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

        public void Error(string message)
        {
            logger.Error(message);
        }

        public void Error(string message, Exception exception)
        {
            logger.Error(exception, message);
        }
    }
}