﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Akka.Actor;
using Akka.Net.Succinctly.Core.WebApi.Controllers;
using Akka.Net.Succinctly.Core.Common;

namespace Akka.Net.Succinctly.Core.WebApi
{
    public partial class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var hocon = HoconLoader.FromFile("akka.net.hocon");
            var actorSystem = ActorSystem.Create("calculator-actor-system", hocon);

            services.AddSingleton(typeof(ActorSystem), (serviceProvider) => actorSystem);
            services.AddSingleton(typeof(ICalculatorActorInstance), typeof(CalculatorActorInstance));
            services.AddMvc();
        }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
