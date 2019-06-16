using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movie.ServiceHost.API.Business;
using Movie.ServiceHost.API.Data;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using Movie.ServiceHost.API.Middlewares;
using NLog.Extensions.Logging;
using NLog.Web;

namespace Movie.ServiceHost.API
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddMemoryCache();

            services.AddTransient<IFilmBusiness, FilmBusiness>();
            services.AddTransient<IFilmRepository, FilmRepository>();

            services.AddSwaggerGen(c =>
              {
                  c.SwaggerDoc("v1", new Info { Title = "ASP.NET Core Web API", Version = "v1" });
              });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddNLog();
            app.AddNLogWeb();

            env.ConfigureNLog("nlog.config");

            loggerFactory.AddFile("C:/@Logs/mobven-task-{Date}.txt");

            app.UseMiddleware(typeof(GlobalExceptionMiddleware));
            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
             {
                 c.SwaggerEndpoint("/swagger/v1/swagger.json", "ASP.NET Core Web API v1");
             });
        }
    }
}
