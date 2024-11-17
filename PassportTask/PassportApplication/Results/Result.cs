using Microsoft.AspNetCore.Mvc;
using System.Net;

using PassportApplication.Errors;
using PassportApplication.Extensions;

namespace PassportApplication.Results
{
    /// <summary>
    /// Result pattern implementation
    /// </summary>
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

        /// <summary>
        /// Returns whether the result contains an error
        /// </summary>
        public bool IsError { get; }
        /// <summary>
        /// Returns whether the result doesn't contain an error
        /// </summary>
        public bool IsSuccess => !IsError;
        /// <summary>
        /// Result error
        /// </summary>
        public Error? Error => _error;
        /// <summary>
        /// Creates Result without error
        /// </summary>
        /// <returns>Result without error</returns>
        public static Result Ok() => new();
        /// <summary>
        /// Creates Result with error
        /// </summary>
        /// <param name="message">Error message</param>
        /// <returns>Result with error</returns>
        public static Error Fail(string message) => new(message);
        /// <summary>
        /// Creates Result with error
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="innerError">Inner error</param>
        /// <returns>Result with error</returns>
        public static Error Fail(string message, Error innerError) => new(message, innerError);
        /// <summary>
        /// Creates Result with error
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="stackTrace">Stack trace</param>
        /// <returns>Result with error</returns>
        public static Error Fail(string message, string stackTrace) => new(message, stackTrace);
        /// <summary>
        /// Creates Result with error
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="statusCode">Status code</param>
        /// <returns>Result with error</returns>
        public static Error Fail(string message, HttpStatusCode statusCode) => new(message, statusCode);

        /// <summary>
        /// Converts Result to ActionResult
        /// </summary>
        /// <param name="result">Result instance</param>
        public static implicit operator ActionResult(Result result) => result.ToActionResult();

        /// <summary>
        /// Converts Error to Result
        /// </summary>
        /// <param name="error">Error instance</param>
        public static implicit operator Result(Error error) => new(error);

        /// <summary>
        /// Matches Result with TResult
        /// </summary>
        /// <typeparam name="TResult">Type</typeparam>
        /// <param name="success">Method to match Result with TResult if Result has no error</param>
        /// <param name="failure">Method to match Result with TResult if Result has an error</param>
        /// <returns>TResult instance</returns>
        public TResult Match<TResult>(Func<TResult> success, Func<Error, TResult> failure)
            => !IsError ? success() : failure(_error!);

        /// <summary>
        /// Asynchronously matches Result with TResult
        /// </summary>
        /// <typeparam name="TResult">Type</typeparam>
        /// <param name="success">Method to asynchronously match Result with TResult if Result has no error</param>
        /// <param name="failure">Method to asynchronously match Result with TResult if Result has an error</param>
        /// <returns>TResult instance</returns>
        public async Task<TResult> MatchAsync<TResult>(Func<Task<TResult>> success, Func<Error, Task<TResult>> failure)
            => !IsError ? await success() : await failure(_error!);
    }
}
