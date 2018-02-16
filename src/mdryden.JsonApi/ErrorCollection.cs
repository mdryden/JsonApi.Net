using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace mdryden.JsonApi
{
    public class ErrorCollection : List<Error>
    {
		public ErrorCollection()
		{

		}

		public ErrorCollection(Error error)
		{
			Add(error);
		}
		
    }
}
