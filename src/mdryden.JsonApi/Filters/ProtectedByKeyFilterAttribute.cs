using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace mdryden.JsonApi.Filters
{
    public class ProtectedByKeyFilterAttribute : ServiceFilterAttribute
	{
		public ProtectedByKeyFilterAttribute()
			: base(typeof(ApiKeyFilterAttribute))
		{

		}
    }
}
