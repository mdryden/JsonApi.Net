using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace mdryden.JsonApi
{
	public class ApiResponseBuilder : IErrors, ILinks, IMeta, IResource, IResources
	{
		IApiResponse baseResponse;
		IApiItemResponse itemResponse;
		IApiCollectionResponse collectionResponse;

		public List<Resource> DataCollection
		{
			get { return collectionResponse.DataCollection; }
			set { collectionResponse.DataCollection = value; }
		}
		public ErrorCollection Errors
		{
			get { return baseResponse.Errors; }
			set
			{
				baseResponse.Errors = value;
				itemResponse.Errors = value;
				collectionResponse.Errors = value;
			}
		}
		public LinkCollection Links
		{
			get { return baseResponse.Links; }
			set
			{
				baseResponse.Links = value;
				itemResponse.Links = value;
				collectionResponse.Links = value;
			}
		}
		public MetaCollection Meta
		{
			get { return baseResponse.Meta; }
			set
			{
				baseResponse.Meta = value;
				itemResponse.Meta = value;
				collectionResponse.Meta = value;
			}
		}
		public Resource Data
		{
			get { return itemResponse.Data; }
			set { itemResponse.Data = value; }
		}

		public ApiResponseBuilder(HttpStatusCode responseCode)
		{
			baseResponse = new ApiResponse { ResponseCode = responseCode };
			itemResponse = new ApiItemResponse { ResponseCode = responseCode };
			collectionResponse = new ApiCollectionResponse { ResponseCode = responseCode };
		}

		private void Validate()
		{
			var hasErrors = Errors?.Count > 0;
			var hasData = Data != null || DataCollection != null;

			if (hasErrors && hasData)
			{
				throw new NotSupportedException("Errors and data must not exist in the same response.");
			}
		}

		public IApiResponse AsResponse()
		{
			return baseResponse;
		}

		public IApiItemResponse AsItemResponse()
		{
			Validate();
			return itemResponse;
		}

		public IApiCollectionResponse AsCollectionResponse()
		{
			Validate();
			return collectionResponse;
		}
	}
}
