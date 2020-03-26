using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using xEventLogger.Interface;
using xFilewriter;
using xFilewriter.Interface;

namespace xEventLogger
{
    public class EventLogger : IEventLogger
    {
        private IFileWriter FileWriter { get; }
        private readonly string directory = $"{Directory.GetCurrentDirectory()}/log/";

        public EventLogger(IFileWriter filewriter)
        {
            if (filewriter == null)
                throw new System.Exception("Error: Filewriter is null");
            FileWriter = filewriter;
        }

        private void Savefile(string text, string fileName, FileMode mode)
        {
            FileWriter.EnsureThatFilePathExists(directory, fileName);
            FileWriter.AppendTextToFile(text, $"{directory}/{fileName}", mode);
        }

        public void LogEvent(string text, string fileName)
        {
            if (Validation.StringIsNullOrEmpty(text))
                throw new Exception("Text string is empty or null");

            if (Validation.StringIsNullOrEmpty(fileName))
                throw new Exception("Give text path is empty or null");

            Savefile(text, fileName, FileMode.Append);
        }

        public async void LogEventAsync(string text, string fileName)
            => await Task.Run(() => LogEvent(text, fileName));

        private string AppendEventToLog<T>(T data, string fileName)
        {
            FileWriter.EnsureThatFilePathExists(directory, fileName);
            var log = JsonConvert.DeserializeObject<List<T>>(GetLogFile(fileName));
            var list = new List<T>();
            if (log != null)
                list = log;
            list.Add((T)data);
            return JsonConvert.SerializeObject(list);
        }

        public void LogEvent(Exception error, string fileName)
        {
            if (error == null)
                throw new Exception("The passed error is null");

            if (Validation.StringIsNullOrEmpty(fileName))
                throw new Exception("Give text path is empty or null");

            var exception = new ExceptionHelper(error);
            var formatedError = (Error)exception.GetFormatedErrorObject();
            var json = AppendEventToLog(formatedError, fileName);
            Savefile(json, fileName, FileMode.Truncate);
        }

        public async void LogEventAsync(Exception error, string fileName)
             => await Task.Run(() => LogEvent(error, fileName));

        public void LogEvent<T>(T data, string fileName)
        {
            if (data == null)
                throw new Exception("The passed error is null");

            if (Validation.StringIsNullOrEmpty(fileName))
                throw new Exception("Give text path is empty or null");

            var json = AppendEventToLog(data, fileName);
            Savefile(json, fileName, FileMode.Truncate);
        }

        public async void LogEventAsync<T>(T data, string fileName)
             => await Task.Run(() => LogEvent(data, fileName));

        public string GetLogFile(string fileName)
        {

            if (Validation.StringIsNullOrEmpty(fileName))
                throw new Exception("The given filepath is null or empty");

            return FileWriter.ReadTextFromFile($"{directory}/{fileName}");
        }

        public async Task<string> GetLogFileAsync(string fileName)
            => await Task.Run(() => GetLogFile(fileName));
    }
}
