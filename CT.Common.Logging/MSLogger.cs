using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;

namespace CT.Common.Logging
{
    public class MSLogger : CT.Common.Logging.Interfaces.ILogger
    {
        LogEntry logEntry;
        public MSLogger ()
        {
            logEntry = new LogEntry();
            Logger.SetLogWriter(new LogWriterFactory().Create());
        }

        public string LogFilePath
        {
            get
            {
                RollingFlatFileTraceListenerData fileTraceListener = LoggingSettings.GetLoggingSettings(new Microsoft.Practices.EnterpriseLibrary.Common.Configuration.SystemConfigurationSource()).TraceListeners.Get("Rolling Flat File Trace Listener") as RollingFlatFileTraceListenerData;
                return fileTraceListener.FileName;
            }
        }

        public void Fatal(string message)
        {
            logEntry.Message = message;
            logEntry.Severity = TraceEventType.Critical;
            Logger.Write(logEntry);
        }

        public void Error(string message)
        {
            logEntry.Message = message;
            logEntry.Severity = TraceEventType.Error;
            Logger.Write(logEntry);
        }

        public void Warn(string message)
        {
            logEntry.Message = message;
            logEntry.Severity = TraceEventType.Warning;
            Logger.Write(logEntry);
        }

        public void Info(string message) 
        {
            logEntry.Message = message;
            logEntry.Severity = TraceEventType.Information;
            Logger.Write(logEntry);
        }

        public void Debug(string message)
        {
            logEntry.Message = message;
            logEntry.Severity = TraceEventType.Verbose;
            Logger.Write(logEntry);
        }

        public void Shutdown()
        {
            Logger.Reset();
        }

        public void Log (string message, LogLevel logLevel)
        {
            logEntry.Message = message;
            switch (logLevel)
            {
                case LogLevel.Fatal:
                    logEntry.Severity = TraceEventType.Critical;
                    break;
                case LogLevel.Error:
                    logEntry.Severity = TraceEventType.Error;
                    break;
                case LogLevel.Warn:
                    logEntry.Severity = TraceEventType.Warning;
                    break;
                case LogLevel.Info:
                    logEntry.Severity = TraceEventType.Information;
                    break;
                case LogLevel.Debug:
                    logEntry.Severity = TraceEventType.Verbose;
                    break;
            }
            Logger.Write(logEntry);
        }
    }
}
