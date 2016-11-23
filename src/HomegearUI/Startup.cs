﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Abstractions.Services;
using HomegearXMLRPCService.Services;
using HomegearXMLRPCService.Options;
using Microsoft.Extensions.Configuration;

namespace HomegearUI
{
    public class Startup
    {

        /// <summary>
        /// Logger used in the 
        /// </summary>
        private readonly ILogger<Startup> logger;
        private readonly IConfigurationRoot configuration;

        /// <summary>
        /// Constructs the startup class and loads the main application configuration.
        /// </summary>
        /// <param name="env">Information about the hosting environment.</param>
        /// <param name="configurationStringsService">Injected configuration strings service instance.</param>
        /// <param name="loggerFactory">Basic startup logger creation factory.</param>
        public Startup(IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger<Startup>();
            this.configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional:true, reloadOnChange:true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddOptions();
            services.Configure<Connection>(configuration.GetSection("Homegear:Connection"));

            services.AddRouting();

            // here we could add specific policies that can be selected at run time.
            services.AddCors();

            services.AddSingleton<IHomegearService, XMLRPCHomegearService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IHomegearService homegear)
        {
            loggerFactory.AddConsole();

            this.logger.LogDebug("Confguration of the request pipeline.");

            if (env.IsDevelopment())
            {
                this.logger.LogDebug("Development environment detected");
                this.logger.LogDebug("Adding the developer exception.");
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseStatusCodePages();

            if (env.IsDevelopment())
            {
                this.logger.LogDebug("Adding CORS.");
                // Add cors for the Aurelia client, which is developed as a separate project and runs on a separate port
                app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            }

            app.UseMvcWithDefaultRoute();

            
        }
    }
}
