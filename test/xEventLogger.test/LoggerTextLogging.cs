using Microsoft.VisualStudio.TestTools.UnitTesting;
using xEventLogger.Interface;
using xFilewriter;

namespace xEventLogger.test
{
    [TestClass]
    public class LoggerTextLogging
    {
        IEventLogger Logger { get; }

        public LoggerTextLogging()
            => Logger = new EventLogger(new FileWriter());


        [TestMethod]
        public void LogText()
        {
            var fileName = "error.txt";
            Logger.LogEvent("Testing Text Log Event", fileName);
            var textFile = Logger.GetLogFile(fileName);
            Assert.IsTrue(textFile.Contains("Testing Text Log Event"));
        }
    }
}
