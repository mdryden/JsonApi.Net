using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace mdryden.JsonApi
{
    public class JsonApiException : Exception
    { 
        public HttpStatusCode Status { get; private set; }

        public JsonApiException(HttpStatusCode status)
        {
            Status = status;
        }

        public JsonApiException(HttpStatusCode status, string message)
            : base(message)
        {
            Status = status;
        }
    }
}
