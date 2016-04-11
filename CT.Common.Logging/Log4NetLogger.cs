using System.Linq;

namespace CT.Common.Logging
{
    public class Log4NetLogger : Interfaces.ILogger
    {
        private readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly log4net.Repository.ILoggerRepository _repo = log4net.LogManager.GetRepository();

        public Log4NetLogger()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public string LogFilePath => _repo.GetAppenders().OfType<log4net.Appender.RollingFileAppender>().Single(fa => fa.Name == "FileAppender").File;

        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
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
                    _logger.Fatal(message);
                    break;
                case LogLevel.Error:
                    _logger.Error(message);
                    break;
                case LogLevel.Warn:
                    _logger.Warn(message);
                    break;
                case LogLevel.Info:
                    _logger.Info(message);
                    break;
                case LogLevel.Debug:
                    _logger.Debug(message);
                    break;
            }
        }
    }
}
