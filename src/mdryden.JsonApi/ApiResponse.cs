using mdryden.JsonApi.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;

namespace mdryden.JsonApi
{

	public class ApiResponse : IApiResponse
	{
		public HttpStatusCode ResponseCode { get; set; }
		
		public ErrorCollection Errors { get; set; }
		
		public LinkCollection Links { get; set; }
		
		public MetaCollection Meta { get; set; }


		public static ApiResponseBuilder WithStatus(HttpStatusCode responseCode)
		{
			return new ApiResponseBuilder(responseCode);
		}

		public static ApiResponseBuilder OK()
		{
			return new ApiResponseBuilder(HttpStatusCode.OK);
		}
		
		public static ApiResponseBuilder Created()
		{
			return new ApiResponseBuilder(HttpStatusCode.Created);
		}
		public static ApiResponseBuilder NoContent()
		{
			return new ApiResponseBuilder(HttpStatusCode.NoContent);
		}

		public static ApiResponseBuilder BadRequest()
		{
			return new ApiResponseBuilder(HttpStatusCode.BadRequest);
		}

		public static ApiResponseBuilder Forbidden()
		{
			return new ApiResponseBuilder(HttpStatusCode.Forbidden);
		}

		public static ApiResponseBuilder NotFound()
		{
			return new ApiResponseBuilder(HttpStatusCode.NotFound);
		}

		public static ApiResponseBuilder InternalServerError()
		{
			return new ApiResponseBuilder(HttpStatusCode.InternalServerError);
		}
		
	}
}
