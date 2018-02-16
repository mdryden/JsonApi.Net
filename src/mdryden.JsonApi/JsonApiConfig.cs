using mdryden.JsonApi;
using mdryden.JsonApi.Filters;
using mdryden.JsonApi.Formatters;
using Microsoft.AspNetCore.Mvc.Formatters;
//using mdryden.JsonApi.Formatters;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class JsonApiConfig
	{
		public static IServiceCollection AddJsonApi(this IServiceCollection services, Action<JsonApiOptions> configureOptions)
		{
			var options = new JsonApiOptions();

			configureOptions.Invoke(options);

			services.AddMvc(mvcOptions =>
			{
				mvcOptions.Filters.Add(typeof(ApiExceptionFilterAttribute));
				//mvcOptions.Filters.AddService<ApiExceptionFilterAttribute>();

				if (options.AddDefaultMetaData != null)
				{
					mvcOptions.Filters.Add(typeof(DefaultMetaDataFilterAttribute));
					//services.AddScoped<DefaultMetaDataFilterAttribute>();
					//mvcOptions.Filters.AddService<DefaultMetaDataFilterAttribute>();
				}

				if (options.AddSelfLinks)
				{
					mvcOptions.Filters.Add(typeof(SelfLinksFilterAttribute));
					//mvcOptions.Filters.AddService<SelfLinksFilterAttribute>();
				}

				mvcOptions.OutputFormatters.Insert(0, new ApiResponseFormatter());
			});

			//services.AddSingleton(options);

			services.AddScoped<ApiKeyFilterAttribute>();
			services.AddSingleton(options.ApiKeys);

			services.AddScoped<IDefaultMetaDataRetriever>(serviceProvider =>
			{
				return new DefaultMetaDataRetriever(options.AddDefaultMetaData);
			});

			return services;
		}


		//      public static IServiceCollection AddJsonApi(this IServiceCollection services)
		//      {
		//          services.AddMvc(option =>
		//          {
		//		option.Filters.Add(typeof(ApiExceptionFilterAttribute));
		//          });

		//          services.AddScoped<ApiExceptionFilterAttribute>();
		//	services.AddScoped<IApiErrorResponseWriter, ApiErrorResponseWriter>();

		//          return services;
		//      }

		//      public static IServiceCollection WithKeys(this IServiceCollection services, ConfigurationSection keysSection)
		//      {
		//	services.Configure<KeyCollection>(keysSection);

		//          services.AddScoped<ApiKeyFilterAttribute>();

		//	services.AddMvc(option =>
		//	{
		//		option.Filters.Add(typeof(ApiKeyFilterAttribute));
		//	});

		//          return services;
		//      }

		//public static IServiceCollection WithKeys(this IServiceCollection services, ConfigurationSection keysSection, Func<ApiKeyOptions> options)
		//{
		//	services.WithKeys(keysSection);
		//	services.AddSingleton(options.Invoke());

		//	return services;
		//}

		//      public static IServiceCollection AddMetaData(this IServiceCollection services, Action<DefaultMetaCollection> defaultMetaAction)
		//      {
		//	DefaultMetaAction = defaultMetaAction;

		//	services.AddMvc(option =>
		//	{
		//		option.Filters.Add(typeof(DefaultMetaDataFilterAttribute));
		//	});

		//	return services;
		//      }


	}
}
