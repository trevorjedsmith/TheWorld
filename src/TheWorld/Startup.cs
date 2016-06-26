using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TheWorld.Services;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.DotNet.InternalAbstractions;
using TheWorld.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using TheWorld.ViewModels;

namespace TheWorld
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940

        public void ConfigureServices(IServiceCollection services)
        {
            //mvc
            services.AddMvc().AddJsonOptions(opt=>
            {
                //json camel case options
                opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            services.AddLogging();

            services.AddEntityFrameworkSqlServer().AddDbContext<WorldContext>();

            //change out with real mail service in production
            services.AddScoped<CoordService>();
            services.AddScoped<IMailService, DebugMailService>();
            services.AddScoped<IWorldRepository, WorldRepository>();
            services.AddScoped<WorldContextSeedData>();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, WorldContextSeedData wcsd,ILoggerFactory loggerFactory)
        {
            //you could add provider to log to a db
            loggerFactory.AddDebug(LogLevel.Warning);

            app.UseStaticFiles();

            AutoMapper.Mapper.Initialize(config =>
            {
                //map both way through anon method
                config.CreateMap<Trip, TripViewModel>().ReverseMap();
                config.CreateMap<Stop, StopViewModel>().ReverseMap();
            });

            app.UseMvc(config => 
            {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "App", action = "index" }
                    );
            });

            wcsd.EnsureSeedData();
        }
    }
}
