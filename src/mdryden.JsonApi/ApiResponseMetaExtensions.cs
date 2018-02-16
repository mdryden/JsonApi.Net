using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mdryden.JsonApi
{
	public static class ApiResponseExtensions
	{
		public static ApiResponse WithMeta(this ApiResponse response, string key, object value)
		{
			response.AddMeta(key, value);
			return response;
		}
		
	}
}
