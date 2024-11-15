using Microsoft.AspNetCore.Mvc;
using System.Net;

using PassportApplication.Errors;
using PassportApplication.Extensions;

namespace PassportApplication.Results
{
    public class Result
    {
        protected readonly Error? _error;

        protected Result()
        {
            IsError = false;
            _error = default;
        }
        protected Result(Error error)
        {
            IsError = true;
            _error = error;
        }

        public bool IsError { get; }
        public bool IsSuccess => !IsError;
        public Error? Error => _error;
        public static Result Ok() => new();
        public static Error Fail(string message) => new(message);
        public static Error Fail(string message, Error innerError) => new(message, innerError);
        public static Error Fail(string message, string stackTrace) => new(message, stackTrace);
        public static Error Fail(string message, HttpStatusCode statusCode) => new(message, statusCode);

        public static implicit operator ActionResult(Result result) => result.ToActionResult();

        public static implicit operator Result(Error error) => new(error);

        public TResult Match<TResult>(Func<TResult> success, Func<Error, TResult> failure)
            => !IsError ? success() : failure(_error!);
    }
}
