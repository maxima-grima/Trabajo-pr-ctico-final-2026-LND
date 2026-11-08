using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Trabajo_Practico_Final_2026.Exceptions;

namespace Trabajo_Practico_Final_2026.Filters
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            int? statusCode = context.Exception switch
            {
                BadRequestException => StatusCodes.Status400BadRequest,
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                ForbiddenException => StatusCodes.Status403Forbidden,
                NotFoundException => StatusCodes.Status404NotFound,
                ConflictException => StatusCodes.Status409Conflict,
                _ => null
            };

            if (statusCode == null)
                return;

            context.Result = new ObjectResult(new { message = context.Exception.Message })
            {
                StatusCode = statusCode
            };
            context.ExceptionHandled = true;
        }
    }
}
