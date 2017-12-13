using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mbsoft.JsonApi
{
    public class JsonApiSerializerSettings : JsonSerializerSettings
    {
        public JsonApiSerializerSettings()
        {
            this.NullValueHandling = NullValueHandling.Ignore;
        }
    }
}
