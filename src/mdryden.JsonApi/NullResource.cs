using mdryden.JsonApi.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace mdryden.JsonApi
{
	[JsonConverter(typeof(NullResourceConverter))]
    public sealed class NullResource : Resource
    {
    }
}
