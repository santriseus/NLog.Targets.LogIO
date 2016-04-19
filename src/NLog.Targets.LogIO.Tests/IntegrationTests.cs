using System;
using NLog.Config;
using NUnit.Framework;

namespace NLog.Targets.LogIO.Tests
{
    [TestFixture, Explicit]
    public class IntegrationTests
    {
        [Test]
        public void SimpleLogTest()
        {
            var logioTarget = new LogIOTarget();
            logioTarget.Node = "${machinename}";
            logioTarget.Stream = "${logger}";
            logioTarget.Layout = "Logger: ${logger}  TID: ${threadid} Message: ${message}";

            var rule = new LoggingRule("*", logioTarget);
            rule.EnableLoggingForLevels(LogLevel.Trace, LogLevel.Fatal);

            var config = new LoggingConfiguration();
            config.LoggingRules.Add(rule);

            LogManager.Configuration = config;

            var logger = LogManager.GetLogger("Example");

            logger.Trace("Hello log.io with trace!");
            logger.Debug("Hello log.io with debug!");
            logger.Info("Hello log.io with info!");
            logger.Warn("Hello log.io with warn!");
            logger.Error("Hello log.io with error!");
            logger.Fatal("Hello log.io with fatal!");

            LogManager.Flush();
        }

        [Test]
        public void ExceptionTest()
        {
            var logioTarget = new LogIOTarget();
            logioTarget.Node = "${machinename}";
            logioTarget.Stream = "${logger}";

            var rule = new LoggingRule("*", logioTarget);
            rule.EnableLoggingForLevel(LogLevel.Error);

            var config = new LoggingConfiguration();
            config.LoggingRules.Add(rule);

            LogManager.Configuration = config;

            var logger = LogManager.GetLogger("Example");

            var exception = new ArgumentException("Some random error message");

            logger.Error(exception, "An exception occured");

            LogManager.Flush();
        }

        [Test]
        public void ReadFromConfigTest()
        {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;

            LogManager.Configuration = new XmlLoggingConfiguration("NLog.Targets.LogIO.Tests.dll.config");

            var logger = LogManager.GetLogger("Example");

            logger.Info("Hello log.io");

            LogManager.Flush();
        }        
    }
}