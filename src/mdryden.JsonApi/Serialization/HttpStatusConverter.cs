using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace mdryden.JsonApi.Serialization
{
	class HttpStatusConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return typeof(HttpStatusCode).Equals(objectType);
		}

		public override bool CanRead => false;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException("HttpStatusConverter.ReadJson is not implemeneted");
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			serializer.Serialize(writer, ((int)value).ToString());
		}
	}
}
