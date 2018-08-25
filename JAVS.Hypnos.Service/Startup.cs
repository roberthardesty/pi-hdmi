﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JAVS.Hypnos.Service.Hubs;
using JAVS.Hypnos.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace JAVS.Hypnos.Service
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSignalR()
                .AddMessagePackProtocol();

            services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin()
                    .AllowCredentials();
            }));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseCors("CorsPolicy");
            }

            app.UseFileServer();
            app.UseMvc();

            app.UseSignalR(routes =>
            {
                routes.MapHub<ApplicationHub>(RouteFor(HubNames.Application));
                routes.MapHub<FaceDetectionHub>(RouteFor(HubNames.FaceDetection));
                routes.MapHub<TestHub>(RouteFor(HubNames.Testing));
            });
        }

        public string RouteFor(string hubName)
        {
            return $"/{hubName}";
        }
    }
}
