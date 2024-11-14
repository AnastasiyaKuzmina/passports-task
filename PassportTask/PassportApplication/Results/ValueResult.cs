using Microsoft.AspNetCore.Mvc;
using PassportApplication.Errors;
using PassportApplication.Errors.Enums;

namespace PassportApplication.Results
{

    public class Result<T> : Result
    {
        public T? Value { get; set; }

        public Result(T? value) : base(new Error())
        { 
            Value = value;
        }

        public Result(Error error) : base(error)
        {
            Value = default;
        }

        public new ActionResult<T> ToActionResult()
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
    }
}
