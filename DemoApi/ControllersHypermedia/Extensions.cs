using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoApi.ControllersHypermedia.Services;
using DemoApi.ControllersHypermedia.Links;

namespace DemoApi.ControllersHypermedia
{
    public static class Extensions
    {
        public static void RegisterStoreServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<HomeLinks>();
            services.AddScoped<BookLinks>();
            services.AddScoped<AuthorLinks>();
            services.AddScoped<IHomeService, EfSqlHomeService>();
            services.AddScoped<IBooksService, EfSqlBooksService>();
        }
    }
}
