using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http.Filters;
using Academy.BattleShip.Web.Areas.Api.Models;
using WebGrease;

namespace Academy.BattleShip.Web.Areas.Api.Filters
{
    /// <summary>
    /// Global Exception Filter Attribute
    /// </summary>
    public class GlobalExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception == null) return;

            var error = context.Exception.GetType().Name;
            var message = context.Exception.GetBaseException().Message;

            context.Response = context.Request.CreateApiResponse(error, message);
            context.Response.StatusCode = HttpStatusCode.InternalServerError;
        }
    }

}