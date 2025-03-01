using Microsoft.AspNetCore.Mvc;
using Sales.Application.Shared;
using Sales.Application.Shared.Enum;
using System.Net;

namespace SalesApi.Converters
{
    public class ActionResultConverter : IActionResultConverter
    {
        public IActionResult Convert<T>(Result<T> result)
        {
            if (result == null)
                return BuildError(ResultResponseKind.InternalServerError, ErrorType.UnknownError, "Something unexpected went wrong", "Please contact support for more details.");

            if (result.ErrorMessage is null)
            {
                if (result.Data is null)
                    return BuildSuccessResultWithoutData(result.Status, result.Message!);

                return BuildSuccessResult(result.Data!, result.Status, result.Message!);
            }

            return BuildError(result.Status, result.ErrorType!.Value, result.ErrorMessage, result.ErrorDetail!);
        }

        private static ObjectResult BuildSuccessResult(object data, ResultResponseKind status, string message)
        {
            var httpStatus = GetSuccessHttpStatusCode(status);

            return new ObjectResult(new
            {
                Data = data,
                Message = message,
                Status = status.ToString()
            })
            {
                StatusCode = (int)httpStatus
            };
        }

        private static ObjectResult BuildSuccessResultWithoutData(ResultResponseKind status, string message)
        {
            var httpStatus = GetSuccessHttpStatusCode(status);

            return new ObjectResult(new
            {
                Message = message,
                Status = status.ToString()
            })
            {
                StatusCode = (int)httpStatus
            };
        }

        private static ObjectResult BuildError(ResultResponseKind status, ErrorType type, string error, string detail)
        {
            var httpStatus = GetErrorHttpStatusCode(status);

            return new ObjectResult(new
            {
                Type = type.ToString(),
                Error = error,
                Detail = detail
            })
            {
                StatusCode = (int)httpStatus
            };
        }

        private static HttpStatusCode GetErrorHttpStatusCode(ResultResponseKind status)
        {
            return status switch
            {
                ResultResponseKind.BadRequest => HttpStatusCode.BadRequest,
                ResultResponseKind.NotFound => HttpStatusCode.NotFound,
                _ => HttpStatusCode.InternalServerError,
            };
        }

        private static HttpStatusCode GetSuccessHttpStatusCode(ResultResponseKind status)
        {
            return status switch
            {
                ResultResponseKind.Success => HttpStatusCode.OK,
                ResultResponseKind.DataPersisted => HttpStatusCode.Created,
                ResultResponseKind.DataAccepted => HttpStatusCode.Accepted,
                _ => HttpStatusCode.OK,
            };
        }
    }
}