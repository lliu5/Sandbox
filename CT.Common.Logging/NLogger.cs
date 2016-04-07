using System.IO;
using NLog;

namespace CT.Common.Logging
{    
    public class NLogger : CT.Common.Logging.Interfaces.ILogger
    {
        private static NLog.Logger logger = LogManager.GetCurrentClassLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public NLogger ()
        {           
        }

        public string LogFilePath
        {
            get
            {
                return Path.GetFullPath(LogManager.Configuration.FindTargetByName<NLog.Targets.FileTarget>("logfile").FileName.Render(new LogEventInfo()));
            }
        }

        public void Fatal(string message)
        {
            logger.Fatal(message);
        }

        public void Error(string message)
        {
            logger.Error(message);
        }

        public void Warn(string message)
        {
            logger.Warn(message);
        }

        public void Info(string message)
        {
            logger.Info(message);
        }

        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void Shutdown()
        {
            LogManager.Shutdown();
        }
        public void Log (string message, LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Fatal:
                    logger.Fatal(message);
                    break;
                case LogLevel.Error:
                    logger.Error(message);
                    break;
                case LogLevel.Warn:
                    logger.Warn(message);
                    break;
                case LogLevel.Info:
                    logger.Info(message);
                    break;
                case LogLevel.Debug:
                    logger.Debug(message);
                    break;
            }
        }
    }
}
