using Microsoft.AspNetCore.Mvc;
using PassportApplication.Errors;
using PassportApplication.Errors.Enums;

namespace PassportApplication.Results
{
    public class Result
    {
        public Error Error { get; protected set; }
        public int StatusCode { get; protected set; }
        public bool IsSuccess => Error.ErrorType == ErrorType.None;

        public Result() 
        { 
            Error = new Error();
            StatusCode = SetStatusCode();
        }

        public Result(Error error)
        {
            Error = error;
            StatusCode = SetStatusCode();
        }

        public ActionResult ToActionResult()
        {
            return new ObjectResult(Error.Message)
            {
                DeclaredType = typeof(string),
                StatusCode = StatusCode
            };
        }

        protected int SetStatusCode()
        {
            switch (Error.ErrorType)
            {
                case ErrorType.None:
                    return StatusCodes.Status200OK;
                case ErrorType.FileDoesNotExist:
                    return StatusCodes.Status500InternalServerError;
                case ErrorType.WrongPassportFormat:
                    return StatusCodes.Status400BadRequest;
                case ErrorType.ControllerNullArgument:
                    return StatusCodes.Status400BadRequest;
                case ErrorType.HttpClientError:
                    return StatusCodes.Status500InternalServerError;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
