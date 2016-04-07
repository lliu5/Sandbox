using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CT.Common.Logging.Test
{
    [TestClass]
    public class Log4NetLoggerTest
    {
        /// <summary>
        /// Need to add <section name="log4net" type="log4net.Config.Log4NetConfigurationSectionHandler, log4net" /> in config file
        /// </summary>
        [TestMethod]
        public void ConfigExistsTest()
        {
            log4net.Config.XmlConfigurator.Configure();
            log4net.Repository.ILoggerRepository repo = log4net.LogManager.GetRepository();
            Assert.IsTrue(repo.Configured);
        }

        /// <summary>
        /// Need to add <root> and <appender></appender> in config file
        /// </summary>
        [TestMethod]
        public void ConfigAppenderExistsTest()
        {
            log4net.Config.XmlConfigurator.Configure();
            log4net.Repository.ILoggerRepository repo = log4net.LogManager.GetRepository();
            Assert.IsFalse(repo.GetAppenders().Count() == 0);
        }

        /// <summary>
        /// Need file appender in config file
        /// </summary>
        [TestMethod]
        public void ConfigFileAppenderExistsTest()
        {
            log4net.Config.XmlConfigurator.Configure();
            log4net.Repository.ILoggerRepository repo = log4net.LogManager.GetRepository();
            Assert.IsFalse(repo.GetAppenders().OfType<log4net.Appender.FileAppender>().Count() == 0);
        }

        /// <summary>
        /// Need ColoredConsoleAppender in config file
        /// </summary>
        [TestMethod]
        public void ConfigConsoleAppenderExistsTest()
        {
            log4net.Config.XmlConfigurator.Configure();
            log4net.Repository.ILoggerRepository repo = log4net.LogManager.GetRepository();
            Assert.IsFalse(repo.GetAppenders().OfType<log4net.Appender.ColoredConsoleAppender>().Count() == 0);
        }

        /// <summary>
        /// To delete file, add to FileAppender <lockingModel type="log4net.Appender.FileAppender+MinimalLock" />
        /// </summary>
        [TestMethod]
        public void ConfigValidFileTest()
        {
            log4net.Config.XmlConfigurator.Configure();
            log4net.Repository.ILoggerRepository repo = log4net.LogManager.GetRepository();
            log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            string filePath = repo.GetAppenders().OfType<log4net.Appender.RollingFileAppender>().Where(fa => fa.Name == "FileAppender").Single().File;
            if (File.Exists(filePath))
                File.Delete(filePath);

            string message = "This is Test Message";
            logger.Error(message);
            log4net.LogManager.Shutdown();
            Assert.IsTrue(File.ReadAllText(filePath).Contains(message));
        }


        [TestMethod]
        public void WriteToFileTest()
        {
            CT.Common.Logging.Log4NetLogger logger = new Log4NetLogger();

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
            CT.Common.Logging.Log4NetLogger logger = new Log4NetLogger();

            logger.Fatal("Fatal message");
            logger.Error("Error message");
            logger.Info("Info message");
            logger.Debug("Debug message");
        }
    }
}
