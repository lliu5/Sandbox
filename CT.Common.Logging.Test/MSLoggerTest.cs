using System.Diagnostics;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CT.Common.Logging.Test
{
    [TestClass]
    public class MsLoggerTest
    {
        /// <summary>
        /// Need to add "section" in config file
        /// Or missing "specialSources"
        /// </summary>
        [TestMethod]
        public void ConfigExistsTest()
        {
            LoggingSettings msConfig = null;
            try
            {
                msConfig = LoggingSettings.GetLoggingSettings(new Microsoft.Practices.EnterpriseLibrary.Common.Configuration.SystemConfigurationSource());
            }
            catch
            {
                Assert.Fail("Error");
            }
            Assert.IsNotNull(msConfig);
        }

        /// <summary>
        /// Need to add "listeners" in config file
        /// </summary>
        [TestMethod]
        public void ConfigListenerExistsTest()
        {
            Assert.IsTrue(LoggingSettings.GetLoggingSettings(new Microsoft.Practices.EnterpriseLibrary.Common.Configuration.SystemConfigurationSource()).TraceListeners.Count > 0);
        }

        /// <summary>
        /// Need Rolling Flat File listener in config file
        /// </summary>
        [TestMethod]
        public void ConfigFileListenerExistsTest()
        {
            Assert.IsNotNull(LoggingSettings.GetLoggingSettings(new Microsoft.Practices.EnterpriseLibrary.Common.Configuration.SystemConfigurationSource()).TraceListeners.Get("Rolling Flat File Trace Listener"));                  
        }


        /// <summary>
        /// Need Console Trace Listener in config file
        /// </summary>
        [TestMethod]
        public void ConfigConsoleAppenderExistsTest()
        {
            Assert.IsTrue(LoggingSettings.GetLoggingSettings(new Microsoft.Practices.EnterpriseLibrary.Common.Configuration.SystemConfigurationSource()).TraceListeners.Get("System Diagnostics Trace Listener").Type == typeof(ConsoleTraceListener));
        }


        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void ConfigValidFileTest()
        {

            RollingFlatFileTraceListenerData fileTraceListener = LoggingSettings.GetLoggingSettings(new Microsoft.Practices.EnterpriseLibrary.Common.Configuration.SystemConfigurationSource()).TraceListeners.Get("Rolling Flat File Trace Listener") as RollingFlatFileTraceListenerData;
            string filePath = fileTraceListener == null ? string.Empty : fileTraceListener.FileName;
            if (File.Exists(filePath))
                File.Delete(filePath);

            string message = "This is Test Message";
            LogEntry logEntry = new LogEntry() { Message = message, Severity = TraceEventType.Critical };
            Logger.SetLogWriter(new LogWriterFactory().Create());
            Logger.Write(logEntry);
            Logger.Reset();

            Assert.IsTrue(File.ReadAllText(filePath).Contains(message));
        }

        [TestMethod]
        public void WriteToFileTest()
        {
            MsLogger logger = new MsLogger();

            string filePath = logger.LogFilePath;
            if (File.Exists(filePath))
                File.Delete(filePath);

            string message = "This is Test Message";
            logger.Fatal(message);
            logger.Shutdown();
            Assert.IsTrue(File.ReadAllText(filePath).Contains(message));
        }

        [TestMethod]
        public void WriteToConsoleTest()
        {
            MsLogger logger = new MsLogger();

            logger.Fatal("Fatal message");
            logger.Error("Error message");
            logger.Info("Info message");
            logger.Debug("Debug message");
        }
    }
}
