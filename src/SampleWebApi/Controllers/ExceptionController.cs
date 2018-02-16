using mdryden.JsonApi;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebApi.Controllers
{
	[Route("exception")]
    public class ExceptionController : Controller
    {

		[HttpGet("")]
		public ApiResponse Get()
		{
			throw new NotImplementedException();
		}
	}
}
