using log4net;
using System;

namespace JARS.Core
{
    public static class Logger
    {
        static readonly ILog log = LogManager.GetLogger("JarsLogs");

        public static void Fatal(string msg, Exception exception = null)
        {
            if (exception == null)
                log.Fatal(msg);
            else
                log.Fatal(msg, exception);
        }

        public static void Error(string msg, Exception exception = null)
        {
            if (exception == null)
                log.Error(msg);
            else
                log.Error(msg, exception);
        }

        public static void Warn(string msg, Exception exception = null)
        {
            if (exception == null)
                log.Warn(msg);
            else
                log.Warn(msg, exception);
        }

        public static void Info(string msg, Exception exception = null)
        {
            if (exception == null)
                log.Info(msg);
            else
                log.Info(msg, exception);
        }

        public static void Debug(string msg, Exception exception = null)
        {
            if (exception == null)
                log.Debug(msg);
            else
                log.Debug(msg, exception);
        }
    }
}
