using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace mbsoft.JsonApi
{
    public class JsonApiException : Exception
    { 
        public HttpStatusCode Code { get; private set; }

        public JsonApiException(HttpStatusCode code)
        {
            this.Code = code;
        }

        public JsonApiException(HttpStatusCode code, string message)
            : base(message)
        {
            this.Code = code;
        }
    }
}
