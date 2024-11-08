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
            if (Error.ErrorType == ErrorType.None)
            {
                return new ObjectResult(Value)
                {
                    DeclaredType = typeof(T),
                    StatusCode = StatusCode
                };
            } 
            else
            {
                return new ObjectResult(Error.Message)
                {
                    DeclaredType = typeof(string),
                    StatusCode = StatusCode
                };
            }
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
                case ErrorType.FileDoesNotExist:
                    return StatusCodes.Status500InternalServerError;
                case ErrorType.WrongPassportFormat:
                    return StatusCodes.Status400BadRequest;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
