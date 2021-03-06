using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WeCare.Application.Exceptions;

namespace WeCare.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is NotFoundException)
        {
            var exception = context.Exception as NotFoundException;
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            context.Result = new JsonResult(new
            {
                Message = exception.Message
            });
        }
        
        if (context.Exception is BadRequestException)
        {
            var exception = context.Exception as BadRequestException;
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new JsonResult(new
            {
                Message = exception.Message,
                Errors = exception.Errors
            });
        }
    }
}