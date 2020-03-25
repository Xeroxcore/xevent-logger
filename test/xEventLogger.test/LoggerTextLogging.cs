using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
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

        [TestMethod]
        public void LogException()
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception error)
            {
                Logger.LogEvent(error, "exception.json");
                var text = Logger.GetLogFile("exception.json");
                var json = JsonConvert.DeserializeObject<List<Error>>(text);
                Assert.AreEqual(json[0].E_Message, "The method or operation is not implemented.");
            }
        }

        [TestMethod]
        public void LogCustomEvents()
        {
            var customEvent = new CustomEvent()
            {
                Event = "Test Logging",
                Message = "Succesfull Event"
            };
            Logger.LogEvent(customEvent, "customevents.json");
            var text = Logger.GetLogFile("customevents.json");
            var json = JsonConvert.DeserializeObject<List<CustomEvent>>(text);
            Assert.AreEqual(json[0].Message, "Succesfull Event");

        }
    }
}
