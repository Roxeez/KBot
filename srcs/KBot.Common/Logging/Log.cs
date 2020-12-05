using System;

namespace KBot.Common.Logging
{
    public static class Log
    {
        public static readonly Logger Logger = new Logger();

        public static void Debug(string message)
        {
            Logger.Debug(message);
        }

        public static void Information(string message)
        {
            Logger.Information(message);
        }

        public static void Warning(string message)
        {
            Logger.Warning(message);
        }

        public static void Error(string message, Exception exception)
        {
            Logger.Error(message, exception);
        }
    }
}