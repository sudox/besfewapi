
using DemoApi.ControllersHypermedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApi.ControllersHypermedia.Services
{
    public interface IBooksService
    {
        Task<GetBooksResponse> GetAsync();
    }
}
