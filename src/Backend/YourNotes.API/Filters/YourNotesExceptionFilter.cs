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
            if (context.Exception is YourNotesBaseException) HandleProjectException(context);
        }


        public void HandleProjectException(ExceptionContext context)
        {
            if (context.Exception is OnValidationException exception)
            {
                context.HttpContext.Response.StatusCode =  (int) HttpStatusCode.BadRequest;

                 context.Result = new BadRequestObjectResult(new ResponseErrorJson(exception.Error));


            }
        }
    }
}
