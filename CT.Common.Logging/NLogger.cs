using System.IO;
using NLog;

namespace CT.Common.Logging
{    
    public class NLogger : Interfaces.ILogger
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string LogFilePath =>
            Path.GetFullPath(LogManager.Configuration.FindTargetByName<NLog.Targets.FileTarget>("logfile").FileName.Render(new LogEventInfo()));

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
            LogManager.Shutdown();
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
