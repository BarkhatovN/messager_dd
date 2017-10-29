using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Messager.Filters
{
    public class ArgumentExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is ArgumentException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    }
}
