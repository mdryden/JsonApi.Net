using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace mdryden.JsonApi
{
    public class Resource
    {
		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("attributes", NullValueHandling = NullValueHandling.Ignore)]
		public object Attributes { get; set; }

		[JsonProperty("links", NullValueHandling = NullValueHandling.Ignore)]
		public LinkCollection Links { get; set; }

		[JsonProperty("meta", NullValueHandling = NullValueHandling.Ignore)]
		public MetaCollection Meta { get; set; }
    }

	public static class ResourceExtensions
	{
		public static Resource WithAttributes(this Resource resource, object attributesObject)
		{
			resource.Attributes = attributesObject;
			return resource;
		}
	}
}
