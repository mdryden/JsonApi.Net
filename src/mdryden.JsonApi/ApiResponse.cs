using mdryden.JsonApi.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;

namespace mdryden.JsonApi
{

	public sealed class ApiResponse : IObjectWithLinks, IObjectWithMeta
	{
		private object data;
		private ErrorCollection errors;

		private ApiResponse(HttpStatusCode responseCode)
		{
			ResponseCode = responseCode;
		}

		[JsonIgnore]
		public HttpStatusCode ResponseCode { get; set; }

		[JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
		public object Data
		{
			get
			{
				return data;
			}
			set
			{
				if (HasErrors())
				{
					throw new NotSupportedException("Reponse with errors cannot contain data.");
				}
				var isResource = value is Resource || value is IEnumerable<Resource> || value is NullResource;

				if (!isResource)
				{
					throw new InvalidOperationException("Data object must be of type 'Resource' or 'Resource[]'");
				}

				data = value;
			}
		}

		[JsonProperty("errors", NullValueHandling = NullValueHandling.Ignore)]
		public ErrorCollection Errors
		{
			get
			{
				return errors;
			}
			set
			{
				if (HasData())
				{
					throw new NotSupportedException("Response with data cannot contain errors.");
				}
				errors = value;
			}
		}

		[JsonProperty("links", NullValueHandling = NullValueHandling.Ignore)]
		public LinkCollection Links { get; set; }

		[JsonProperty("meta", NullValueHandling = NullValueHandling.Ignore)]
		public MetaCollection Meta { get; set; }


		public static ApiResponse Create(HttpStatusCode responseCode = HttpStatusCode.OK)
		{
			return new ApiResponse(responseCode);
		}


		public bool HasErrors()
		{
			return Errors?.Count > 0;
		}

		public bool HasData()
		{
			return Data != null;
		}

		public bool HasContent()
		{
			return HasData() || HasErrors();
		}
	}
}
