using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RowHouseTurnManagement.Application;
using RowHouseTurnManagement.DB.Cosmos;

namespace UI.MobileAppService
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddTransient<IRegistrationService, RegistrationService>();
            services.AddTransient<IApartmentRepository>(CreateApartmentRepository);
        }

        public void ConfigureDevelopment(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile("Logs/RowHouseTurnManagement-{Date}.txt");
            Configure(app, env, loggerFactory);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseMvc();
        }

        private ApartmentRepository CreateApartmentRepository(IServiceProvider serviceProvider)
        {
            IConfigurationSection cosmosDbConfig = Configuration.GetSection("CosmosDB");
            return new ApartmentRepository(new Uri(cosmosDbConfig["EndpointUrl"]), cosmosDbConfig["AuthenticationKey"]);
        }
    }
}
