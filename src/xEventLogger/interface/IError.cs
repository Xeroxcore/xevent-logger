namespace xEventLogger.Interface
{
    public interface IError
    {
        string E_Date { get; set; }
        string E_Class { get; set; }
        string E_Method { get; set; }
        int E_Row { get; set; }
        string E_Message { get; set; }
    }
}