using Microsoft.AspNetCore.Diagnostics;
using Sales.Application.Shared;
using Sales.Application.Shared.Enum;

namespace SalesApi.ExceptionsHandler
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var errorResponse = new
            {
                ErrorType = ErrorType.UnknownError.ToString(),
                ErrorMessage = Consts.SomeErrorOccurred,
                ErrorDetail = exception.Message
            };

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await httpContext.Response.WriteAsJsonAsync(errorResponse, cancellationToken);

            return true;
        }
    }
}