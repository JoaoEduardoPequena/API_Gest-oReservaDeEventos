using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Exceptions
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var problemDetails = new ProblemDetails();
            problemDetails.Instance = httpContext.Request.Path;

            if (exception is FluentValidation.ValidationException fluentException)
            {
                problemDetails.Title = "ocorreu um ou mais erros de validação.";
                problemDetails.Detail = exception.Message;
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                List<string> validationErrors = new List<string>();
                foreach (var error in fluentException.Errors)
                {
                    validationErrors.Add(error.ErrorMessage);
                }
                problemDetails.Extensions.Add("errors", validationErrors);
                logger.LogError("{ProblemDetailsTitle}", problemDetails);
            }

            else
            {
                problemDetails.Title = exception.Message;
                problemDetails.Detail = exception.Message;
            }

            logger.LogError("{Detalhe do erro}", problemDetails);

            problemDetails.Status = httpContext.Response.StatusCode;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken).ConfigureAwait(false);
            return true;
        }

    }
}
