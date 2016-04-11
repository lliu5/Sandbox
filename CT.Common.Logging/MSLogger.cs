using System.Diagnostics;
using CT.Common.Logging.Interfaces;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;

namespace CT.Common.Logging
{
    public class MsLogger : ILogger
    {
        private readonly LogEntry _logEntry;

        public MsLogger()
        {
            _logEntry = new LogEntry();
            Logger.SetLogWriter(new LogWriterFactory().Create());
        }

        public string LogFilePath
        {
            get
            {
                var fileTraceListener =
                    LoggingSettings.GetLoggingSettings(new SystemConfigurationSource())
                        .TraceListeners.Get("Rolling Flat File Trace Listener") as RollingFlatFileTraceListenerData;
                return fileTraceListener == null ? string.Empty : fileTraceListener.FileName;
            }
        }

        public void Fatal(string message)
        {
            _logEntry.Message = message;
            _logEntry.Severity = TraceEventType.Critical;
            Logger.Write(_logEntry);
        }

        public void Error(string message)
        {
            _logEntry.Message = message;
            _logEntry.Severity = TraceEventType.Error;
            Logger.Write(_logEntry);
        }

        public void Warn(string message)
        {
            _logEntry.Message = message;
            _logEntry.Severity = TraceEventType.Warning;
            Logger.Write(_logEntry);
        }

        public void Info(string message)
        {
            _logEntry.Message = message;
            _logEntry.Severity = TraceEventType.Information;
            Logger.Write(_logEntry);
        }

        public void Debug(string message)
        {
            _logEntry.Message = message;
            _logEntry.Severity = TraceEventType.Verbose;
            Logger.Write(_logEntry);
        }

        public void Shutdown()
        {
            Logger.Reset();
        }

        public void Log(string message, LogLevel logLevel)
        {
            _logEntry.Message = message;
            switch (logLevel)
            {
                case LogLevel.Fatal:
                    _logEntry.Severity = TraceEventType.Critical;
                    break;
                case LogLevel.Error:
                    _logEntry.Severity = TraceEventType.Error;
                    break;
                case LogLevel.Warn:
                    _logEntry.Severity = TraceEventType.Warning;
                    break;
                case LogLevel.Info:
                    _logEntry.Severity = TraceEventType.Information;
                    break;
                case LogLevel.Debug:
                    _logEntry.Severity = TraceEventType.Verbose;
                    break;
            }
            Logger.Write(_logEntry);
        }
    }
}