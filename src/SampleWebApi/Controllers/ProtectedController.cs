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
		public IApiCollectionResponse Get()
		{
			var values = new[] { "one", "two", "three" };

			return ApiResponse.OK().WithResources("string", values, (item) => item).AsCollectionResponse();
		}
    }
}
