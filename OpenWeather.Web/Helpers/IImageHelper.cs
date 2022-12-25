using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace OpenWeather.Web.Helpers
{
    public interface IImageHelper
    {
        Task<string> UploadImageAsync(IFormFile imageFile, string folder);
    }
}
