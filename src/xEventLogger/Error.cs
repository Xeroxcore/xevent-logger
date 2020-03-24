using xEventLogger.Interface;

namespace xEventLogger
{
    public sealed class Error : IError
    {
        public string E_Date { get; set; }
        public string E_Class { get; set; }
        public string E_Method { get; set; }
        public int E_Row { get; set; }
        public string E_Message { get; set; }
    }
}