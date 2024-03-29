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
                exception.Message
            });
        }
        
        if (context.Exception is BadRequestException)
        {
            var exception = context.Exception as BadRequestException;
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new JsonResult(new
            {
                exception.Message,
                exception.Errors
            });
        }

        if (context.Exception is UnauthorizedException)
        {
            var exception = context.Exception as UnauthorizedException;
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Result = new JsonResult(new
            {
                exception.Message
            });
        }
        
        if (context.Exception is UnprocessableEntityException)
        {
            var exception = context.Exception as UnprocessableEntityException;
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
            context.Result = new JsonResult(new
            {
                exception.Message,
                exception.Errors
            });
        }

        if (context.Exception is GoneException)
        {
            var exception = context.Exception as GoneException;
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Gone;
            context.Result = new JsonResult(new
            {
                exception.Message
            });
        }
        
        if (context.Exception is ConflictException)
        {
            var exception = context.Exception as ConflictException;
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
            context.Result = new JsonResult(new
            {
                exception.Message
            });
        }
        
        if (context.Exception is ForbiddenException)
        {
            var exception = context.Exception as ForbiddenException;
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            context.Result = new JsonResult(new
            {
                exception.Message
            });
        }
    }
    
}