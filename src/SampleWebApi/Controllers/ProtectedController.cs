using mdryden.JsonApi;
using mdryden.JsonApi.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebApi.Controllers
{
	[Route("protected")]
	[ProtectedByKeyFilter]
    public class ProtectedController : Controller
    {
		[HttpGet("")]
		public ApiResponse Get()
		{
			var values = new[] { "one", "two", "three" };

			return ApiResponse.Create().WithResources("string", values, (item) => item);
		}
    }
}
