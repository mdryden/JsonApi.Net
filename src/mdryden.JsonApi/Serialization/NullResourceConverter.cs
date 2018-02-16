using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace mdryden.JsonApi.Serialization
{
	class NullResourceConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return typeof(NullResource).Equals(objectType);
		}

		public override bool CanRead => false;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			serializer.Serialize(writer, null);
		}
	}
}
