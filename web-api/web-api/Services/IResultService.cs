using web_api.Models;

namespace web_api.Services
{
    public interface IResultService
    {
        Task<Result> CalculateAsync(IFormFile file);

        Task<int> CalculateFromStreamAsync(StreamReader streamReader);
    }
}
