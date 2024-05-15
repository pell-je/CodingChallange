using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace JobTargetCodingChallange.Controllers
{

    public class ControllerExceptionFilter : IExceptionFilter
    {

        private readonly ILogger<ControllerExceptionFilter> _logger;

        public ControllerExceptionFilter(ILogger<ControllerExceptionFilter> logger)
        {
            _logger = logger;
        }


        public void OnException(ExceptionContext context)
        {

            _logger.LogError(context.Exception, $"{context.ActionDescriptor.DisplayName} failed");

            switch (context.Exception)
            {
                case ValidationException _:
                case InvalidOperationException _:
                case ArgumentException _:
                    context.Result = new BadRequestObjectResult(context.Exception.Message);
                    context.ExceptionHandled = true;
                    break;

                default:
                    context.Result = new ObjectResult(context.Exception.Message)
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                    context.ExceptionHandled = true;
                    break;
            }

        }
    }
}
