using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mdryden.JsonApi
{
    public class ApiKey
    {
        public Guid Key { get; set; }

        public bool Revoked { get; set; }

        public string Details { get; set; }
    }
}
