using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserMgt.Business.Logger
{
    public class NlogLoggerManagerRepository : ILoggerManagerRepository
    {
        private static NLog.ILogger logger = LogManager.GetCurrentClassLogger();

        public void LogDebug(string message)
        {
            logger.Debug(message);
        }
        public void LogError(string message)
        {
            logger.Error(message);
        }
        public void LogInformation(string message)
        {
            logger.Info(message);
        }
        public void LogWarn(string message)
        {
            logger.Warn(message);
        }

        //NOTE NOTE: you cannot override method that is not marked virtual, abstract, override

    }
}
