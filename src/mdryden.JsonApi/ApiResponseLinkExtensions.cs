using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mdryden.JsonApi
{
	public static class ApiResponseLinkExtensions
	{
		public static ApiResponse WithLink(this ApiResponse response, string key, string href)
		{
			response.AddLink(key, href);
			return response;
		}

		public static ApiResponse WithLink(this ApiResponse response, string key, string href, Action<Link> linkConfig)
		{
			var link = new Link { Href = href };
			linkConfig.Invoke(link);
			response.AddLink(key, link);
			return response;
		}

		

	}
}
