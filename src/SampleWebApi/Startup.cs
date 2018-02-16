using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mdryden.JsonApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleWebApi.Controllers;

namespace SampleWebApi
{
    public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }
		
		public void ConfigureServices(IServiceCollection services)
        {
			services.AddJsonApi(options =>
			{
				options.AddSelfLinks = true;
				options.ConfigureKeys = keys =>
				{
					keys.ConfigureFrom(Configuration.GetSection("JsonApiKeys"));
				};
				options.AddDefaultMetaData = meta =>
				{
					meta.Add("version", "0.0.0");
					meta.Add("product", "SampleWebApi");
				};
			});

		}
		
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
