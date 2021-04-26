using CacheCow.Server;
using CacheCow.Server.Core.Mvc;
using DemoApi.Controllers;
using DemoApi.ControllersHypermedia;
using DemoApi.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(b => b.AddDefaultPolicy(x =>
            {
                x.AllowAnyHeader();
                x.AllowAnyMethod();
                x.AllowAnyOrigin();
                x.WithExposedHeaders("ETag");
            }));

            services.AddDbContext<BooksDataContext>(builder =>
            {
                builder.UseSqlServer(Configuration.GetConnectionString("books"));
            });
            services.AddControllers().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.IgnoreNullValues = true;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DemoApi", Version = "v1" });
            });

            services.AddHttpCachingMvc();
            //  services.AddQueryProviderForViewModel<GetAuthorDetailsResponse, TimedEtagQueryAuthor>(false);

            //services.AddResponseCaching(c => 
            //{
            //    c.MaximumBodySize = 2048;
            //    c.SizeLimit = 50;
            //});

          
            services.RegisterStoreServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseCors();
            app.UseResponseCaching(); // has to come after UseCors()
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DemoApi v1"));

            app.UseRouting();

            app.UseAuthorization();

            app.Use(async (context, next) =>
            {
                await Task.Delay(Configuration.GetValue<int>("delay"));
                await next();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
