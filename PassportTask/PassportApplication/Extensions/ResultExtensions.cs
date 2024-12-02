using Microsoft.AspNetCore.Mvc;
using PassportApplication.Errors;
using PassportApplication.Exceptions.Dto;
using PassportApplication.Results;
using PassportApplication.Results.Generic;

namespace PassportApplication.Extensions
{
    /// <summary>
    /// Result extensions
    /// </summary>
    public static class ResultExtensions
    {
        /// <summary>
        /// Converts bool to Result
        /// </summary>
        /// <param name="condition">Condition: if true then Ok, if false then Fail</param>
        /// <param name="errorMessage">Error message</param>
        /// <returns></returns>
        public static Result ToResult(this bool condition, string errorMessage)
        => condition
            ? Result.Ok()
            : Result.Fail(errorMessage);

        /// <summary>
        /// Converts Result to ActionResult
        /// </summary>
        /// <param name="result">Result instance</param>
        /// <returns>ActionResult instance</returns>
        public static ActionResult ToActionResult(this Result result)
        {
            return result.IsSuccess
                ? new OkResult()
                : ErrorResult(result.Error!);
        }

        /// <summary>
        /// Converts Result<typeparamref name="T"/> to ActionResult<typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">Result type</typeparam>
        /// <param name="result">Result</param>
        /// <returns></returns>
        public static ActionResult<T> ToActionResult<T>(this Result<T> result)
        {
            return result.Match<ActionResult<T>>(
                value => new OkObjectResult(value),
                error => new BadRequestObjectResult(new ExceptionDto(error)));
        }

        private static ActionResult ErrorResult(Error error)
        {
            BadRequestObjectResult errorResult = new(new ExceptionDto(error));

            if (error.StatusCode.HasValue)
            {
                errorResult.StatusCode = (int)error.StatusCode.Value;

            }

            return errorResult;
        }
    }
}
