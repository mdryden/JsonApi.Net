using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace mdryden.JsonApi
{
    public interface IErrors
	{
		[JsonProperty("errors", NullValueHandling = NullValueHandling.Ignore)]
		ErrorCollection Errors { get; set; }
	}
}
