//using mbsoft.cfl.RosterService.v1.Core;
//using mbsoft.cfl.RosterService.v1.Models;
//using mbsoft.JsonApi.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace mbsoft.JsonApi.Controllers
//{
//    public class JsonApiController : Controller
//    {
//        public override void OnActionExecuting(ActionExecutingContext context)
//        {
//            // check api key
//            base.OnActionExecuting(context);
//        }

//        public override void OnActionExecuted(ActionExecutedContext context)
//        {
//            if (context.Exception != null)
//            {
//                var result = JsonApiResponseCreator.CreateErrorResponse(context.Exception);
//                var json = JsonConvert.SerializeObject(result, new JsonApiSerializerSettings());

//                context.Result = Content(json, JsonApiConstants.ContentType);
//                context.ExceptionHandled = true;
//            }
//        }

//    }
//}
