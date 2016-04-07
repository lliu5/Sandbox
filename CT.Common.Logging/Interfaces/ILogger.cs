using System;

namespace CT.Common.Logging.Interfaces
{
    interface ILogger
    {
        string LogFilePath { get; }

        void Fatal(string message);

        void Error(string message);

        void Warn(string message);

        void Info(string message);

        void Debug(string message);

        void Shutdown();

        void Log(string message, LogLevel LogLevel);
    }
}
