using OpenWeather.Mobile.Models;
using System.Threading.Tasks;

namespace OpenWeather.Mobile.Services
{
    public interface IApiService
    {
        Task<Response> GetListAsync<T>(string urlBase, string api, string controller);

        Task<Response> GetUser(string urlBase, string api, string controller);

        Task<Response> CheckUser(string urlBase, string api, string controller);
    }
}
