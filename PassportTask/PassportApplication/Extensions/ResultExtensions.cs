using Microsoft.AspNetCore.Mvc;
using PassportApplication.Errors;
using PassportApplication.Exceptions.Dto;
using PassportApplication.Results;
using PassportApplication.Results.Generic;

namespace PassportApplication.Extensions
{
    public static class ResultExtensions
    {
        public static Result ToResult(this bool condition, string errorMessage)
        => condition
            ? Result.Ok()
            : Result.Fail(errorMessage);

        public static ActionResult ToActionResult(this Result result)
        {
            return result.IsSuccess
                ? new OkResult()
                : ErrorResult(result.Error!);
        }

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
