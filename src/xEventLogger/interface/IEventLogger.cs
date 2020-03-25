using System;
using System.Threading.Tasks;

namespace xEventLogger.Interface
{
    public interface IEventLogger
    {
        void LogEvent(string text, string fileName);
        void LogEvent(Exception error, string fileName);
        void LogEventAsync(string text, string fileName);
        void LogEventAsync(Exception error, string fileName);
        Task<string> GetLogFileAsync(string fileName);
        string GetLogFile(string fileName);
        void LogEvent<T>(T data, string fileName);
        void LogEventAsync<T>(T data, string fileName);
    }
}