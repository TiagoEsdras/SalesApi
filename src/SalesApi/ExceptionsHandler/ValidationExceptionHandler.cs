using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Sales.Application.Shared;
using Sales.Application.Shared.Enum;

namespace SalesApi.ExceptionsHandler
{
    public class ValidationExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not ValidationException validationException)
                return false;

            var errorResponse = new
            {
                ErrorType = ErrorType.InvalidData.ToString(),
                ErrorMessage = string.Format(Consts.FieldContainInvalidValue, string.Join(", ", validationException.Errors.Select(it => it.PropertyName))),
                ErrorDetail = string.Join(", ", validationException.Errors.Select(it => it.ErrorMessage))
            };

            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            await httpContext.Response.WriteAsJsonAsync(errorResponse, cancellationToken);

            return true;
        }
    }
}