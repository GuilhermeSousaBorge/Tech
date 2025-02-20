using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Tech.Exception;
using TechDto.Response;

namespace Tech.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is TechException techException)
            {
                context.HttpContext.Response.StatusCode = (int)techException.GetStatusCode();
                context.Result = new ObjectResult(new ErrorsResponseDto
                {
                    Errors = techException.GetErrorMessage()
                });
            }
            else
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Result = new ObjectResult(new ErrorsResponseDto
                {
                    Errors = ["Internal Server Error!"]
                });
            }
        }
    }
}
