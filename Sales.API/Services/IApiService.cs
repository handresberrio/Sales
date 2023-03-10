using Sales.Shared.ResponsesApi;

namespace Sales.API.Services
{
    public interface IApiService
    {
        Task<ResponseGeneric> GetListAsync<T>(string servicePrefix, string controller);
    }
}
