namespace xEventLogger.Interface
{
    public interface IExceptionHelper
    {
        string GetFormatedErrorMessage(bool logAsJson);
        IError GetFormatedErrorObject();
    }
}