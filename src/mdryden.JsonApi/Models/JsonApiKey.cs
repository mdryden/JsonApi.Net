using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mbsoft.JsonApi.Models
{
    public class JsonApiKey
    {
        public Guid Key { get; set; }

        public bool Revoked { get; set; }

        public string Details { get; set; }
    }
}
