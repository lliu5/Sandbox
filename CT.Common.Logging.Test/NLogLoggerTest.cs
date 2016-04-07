using System;
using System.Linq;
using System.IO;
using NLog;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CT.Common.Logging.Test
{
    [TestClass]
    public class NLogLoggerTest
    {
        /// <summary>
        /// Need <section name="nlog" type="NLog.Config.ConfigSectionHandler, NLog"/>
        /// Need <nlog></nlog>
        /// </summary>
        [TestMethod]
        public void ConfigExistsTest()
        {
            NLog.Config.LoggingConfiguration config = null;
            try
            {
                config = LogManager.Configuration;
            }
            catch (Exception ex)
            {
                Assert.Fail("Error");
            }
            Assert.IsNotNull(config);
        }

        /// <summary>
        /// Need to add target in config file
        ///     <targets>
        ///         <target name = "logfile" xsi:type="File" fileName="NLogFile.txt" layout="${date:format=yyyyMMddHHmmss} ${message}" />
        ///     </targets>
        /// </summary>
        [TestMethod]
        public void ConfigTargetExistsTest()
        {
            Assert.IsTrue(LogManager.Configuration.AllTargets.Count > 0);
        }

        /// <summary>
        /// Need file target in config file
        /// </summary>
        [TestMethod]
        public void ConfigFileTargetExistsTest()
        {
            Assert.IsTrue(LogManager.Configuration.AllTargets.Where(t => t.Name == "logfile").ToList().Count > 0);
        }

        /// <summary>
        /// Need Console Target in config file
        /// </summary>
        [TestMethod]
        public void ConfigConsoleAppenderExistsTest()
        {
            Assert.IsTrue(LogManager.Configuration.AllTargets.Where(t => t.Name == "console").ToList().Count > 0);
        }


        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void ConfigValidFileTest()
        {
            string filePath = Path.GetFullPath(LogManager.Configuration.FindTargetByName<NLog.Targets.FileTarget>("logfile").FileName.Render(new LogEventInfo()));
            if (File.Exists(filePath))
                File.Delete(filePath);

            string message = "This is Test Message";
            NLog.Logger logger = LogManager.GetCurrentClassLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            logger.Error(message);
            NLog.LogManager.Shutdown();
            Assert.IsTrue(File.ReadAllText(filePath).Contains(message));
        }

        [TestMethod]
        public void WriteToFileTest()
        {
            CT.Common.Logging.NLogger logger = new CT.Common.Logging.NLogger();

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
            CT.Common.Logging.NLogger logger = new CT.Common.Logging.NLogger();

            logger.Fatal("Fatal message");
            logger.Error("Error message");
            logger.Info("Info message");
            logger.Debug("Debug message");
        }
    }
}
