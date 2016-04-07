using System.Linq;

namespace CT.Common.Logging
{
    public class Log4NetLogger : CT.Common.Logging.Interfaces.ILogger
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private log4net.Repository.ILoggerRepository repo = log4net.LogManager.GetRepository();

        public Log4NetLogger()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public string LogFilePath
        {
            get
            {            
                return repo.GetAppenders().OfType<log4net.Appender.RollingFileAppender>().Where(fa => fa.Name == "FileAppender").Single().File;
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
            log4net.LogManager.Shutdown();
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
