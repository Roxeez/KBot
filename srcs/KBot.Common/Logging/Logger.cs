using System;
using System.Reflection;
using Serilog;
using Serilog.Core;

namespace KBot.Common.Logging
{
    public sealed class Logger
    {
        private readonly ILogger logger;

        public Logger(string name, params ILogEventSink[] sinks)
        {
            LoggerConfiguration configuration = new LoggerConfiguration()
                #if(DEBUG)
                .MinimumLevel.Debug()
                #else
                .MinimumLevel.Debug()
                #endif
                .WriteTo.Console()
                .WriteTo.File($"KBot/logs/{name}-.txt", rollingInterval: RollingInterval.Day);

            if (sinks != null)
            {
                foreach (ILogEventSink sink in sinks)
                {
                    configuration.WriteTo.Sink(sink);
                }
            }

            logger = configuration.CreateLogger();

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