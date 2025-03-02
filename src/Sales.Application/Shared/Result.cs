using Sales.Application.Shared.Enum;

namespace Sales.Application.Shared
{
    public class Result<T>
    {
        public T? Data { get; private set; }
        public string? Message { get; private set; }
        public ErrorType? ErrorType { get; private set; }
        public string? ErrorMessage { get; private set; }
        public string? ErrorDetail { get; private set; }
        public ResultResponseKind Status { get; private set; }

        private Result(ResultResponseKind status, T data, string message)
        {
            Data = data;
            Message = message;
            Status = status;
        }

        private Result(ResultResponseKind status, ErrorType errorType, string errorMessage, string errorDetail)
        {
            ErrorType = errorType;
            ErrorMessage = errorMessage;
            ErrorDetail = errorDetail;
            Status = status;
        }

        public static Result<T> Success(T data, string message) => new(ResultResponseKind.Success, data, message);

        public static Result<T> Persisted(T data, string message) => new(ResultResponseKind.DataPersisted, data, message);

        public static Result<T> BadRequest(ErrorType errorType, string errorMessage, string errorDetail) => new(ResultResponseKind.BadRequest, errorType, errorMessage, errorDetail);

        public static Result<T> NotFound(ErrorType errorType, string errorMessage, string errorDetail) => new(ResultResponseKind.NotFound, errorType, errorMessage, errorDetail);
    }
}