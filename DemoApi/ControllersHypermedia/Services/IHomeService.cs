using DemoApi.ControllersHypermedia.Models;
using System.Threading.Tasks;

namespace DemoApi.ControllersHypermedia.Services
{
    public interface IHomeService
    {
        Task<StoreGetResponse> GetAsync();
    }
}