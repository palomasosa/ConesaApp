
namespace ConesaApp.Client.Pages.Services
{
    public interface IHttpService
    {
        Task<HttpRespuesta<object>> Delete(string url);
        Task<HttpRespuesta<T>> Get<T>(string url);
        Task<HttpRespuesta<T>> Post<T>(string url, T enviar);
        Task<HttpRespuesta<T>> Put<T>(string url, T enviar);
    }
}