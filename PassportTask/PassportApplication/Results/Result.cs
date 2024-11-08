using Microsoft.AspNetCore.Mvc;
using PassportApplication.Errors;
using PassportApplication.Errors.Enums;

namespace PassportApplication.Results
{

    public class Result<T>
    {
        public T? Value { get; set; }
        public Error Error { get; set; }
        public int StatusCode { get; }

        public Result(T? value)
        { 
            Value = value;
            Error = new Error(ErrorType.None);
            StatusCode = SetStatusCode();
        }

        public Result(Error error)
        {
            Value = default;
            Error = error;
            StatusCode = SetStatusCode();
        }

        public ActionResult<T> ToActionResult()
        {
            return new ObjectResult(Value)
            {
                DeclaredType = typeof(T),
                StatusCode = StatusCode

            };
        }

        public static implicit operator Result<T>(T? result)
        {
            return new Result<T>(result);
        }

        public static implicit operator Result<T>(Error error)
        {
            return new Result<T>(error);
        }

        private int SetStatusCode()
        {
            switch (Error.ErrorType)
            {
                case ErrorType.None:
                    return StatusCodes.Status200OK;
                default:
                    throw new NotImplementedException();
            }
        }

    }
}
