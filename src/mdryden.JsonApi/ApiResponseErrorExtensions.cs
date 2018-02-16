using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mdryden.JsonApi
{
	public static class ApiResponseErrorExtensions
	{
		public static void AddError(this ApiResponse response, Error error)
		{
			if (response.Errors == null)
			{
				response.Errors = new ErrorCollection();
			}

			response.Errors.Add(error);			
		}
		

		public static ApiResponse WithError(this ApiResponse response, Action<Error> errorConfig)
		{
			var error = new Error();
			errorConfig.Invoke(error);
			response.AddError(error);
			return response;
		}
		

	}
}
