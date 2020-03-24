using System;
using System.Diagnostics;
using Newtonsoft.Json;
using xEventLogger.Interface;

namespace xEventLogger
{
    public class ExceptionHelper : IExceptionHelper
    {
        private Exception _Error { get; set; }

        public ExceptionHelper(Exception error)
            => _Error = error;

        private StackFrame GetStackFrame(StackTrace stack, int index)
        {
            if (stack != null && index > -1)
                return stack.GetFrame(index);
            else
                throw new Exception("Error:StackFrame validation failed: Class=ExceptionHelper Function=GetStackFrame Row=14");
        }

        private StackTrace GetStackTrace(bool NeedFileInfo = true)
            => new StackTrace(_Error, NeedFileInfo);

        private StackFrame GetErrorFrame()
        {
            var stackTrace = GetStackTrace();
            var StackFrame = GetStackFrame(stackTrace, 0);
            return StackFrame;
        }

        private int GetRowThatTrewException()
            => GetErrorFrame().GetFileLineNumber();

        private string GetMethodThatTrewException()
            => GetErrorFrame().GetMethod().Name;

        private string GetClassThatTrewException()
            => GetErrorFrame().GetMethod().ReflectedType.Name;
        private string GetMessage()
            => _Error.Message;

        private string GetDate()
            => DateTime.Now.ToString("yyyy-MM-dd HH:mm tt;");

        private string GetErrorAsJson()
            => JsonConvert.SerializeObject(GetFormatedErrorObject());

        private string GetErrorAsText()
            => string.Format(
                $"Date:{GetDate()}; ErrorClass:{GetClassThatTrewException()} " +
                $"Method:{GetMethodThatTrewException()}; Row:{GetRowThatTrewException()} " +
                $"Message:{GetMessage()}");

        public string GetFormatedErrorMessage(bool logAsJson)
        {
            var error = GetErrorAsText();
            if (logAsJson)
                error = GetErrorAsJson();
            return error;
        }

        public IError GetFormatedErrorObject()
        {
            return new Error
            {
                E_Date = GetDate(),
                E_Class = GetClassThatTrewException(),
                E_Method = GetMethodThatTrewException(),
                E_Row = GetRowThatTrewException(),
                E_Message = GetMessage()
            };
        }
    }
}