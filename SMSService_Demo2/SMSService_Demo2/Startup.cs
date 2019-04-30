using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SMSService_Demo2.Interfaces;
using SMSService_Demo2.Provider;

namespace SMSService_Demo2
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // **********************************************************
            // DJR: 4/17/2019
            // this is for the Request Formatter (RawRequestFormatter.cs)
            // adds the request formatter to the services
            services.AddMvc(o => o.InputFormatters.Insert(0, new RawRequestBodyFormatter()));

            // **********************************************************
            // DRJ: 4/15/2019
            // this adds the DI for the SMSProvider
            services.AddSingleton<ISMSProvider, SMSProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseStaticFiles();
        }
    }
}
