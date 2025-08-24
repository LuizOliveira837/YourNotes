using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using YourNotes.Communication.Responses;
using YourNotes.Exception.Exceptions;

namespace YourNotes.API.Filters
{
    public class YourNotesExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is YourNotesBaseException baseException) HandleProjectException(context);
        }


        public void HandleProjectException(ExceptionContext context)
        {
            var exception = (YourNotesBaseException)context.Exception;
            context.HttpContext.Response.StatusCode = exception.GetStatusCode();

            context.Result = new ObjectResult(new ResponseErrorJson(exception.GetMessage()));

        }
    }
}
