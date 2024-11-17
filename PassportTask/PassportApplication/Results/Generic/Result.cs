using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

using PassportApplication.Errors;
using PassportApplication.Extensions;

namespace PassportApplication.Results.Generic
{
    /// <summary>
    /// Generic result pattern implementation
    /// </summary>
    /// <typeparam name="TValue">Result value type</typeparam>
    public class Result<TValue> : Result
    {
        private Result(TValue value)
        {
            Value = value;
        }

        protected Result(Error error) : base(error)
        {
            Value = default;
        }

        /// <summary>
        /// Result value
        /// </summary>
        public TValue? Value { get; }

        /// <summary>
        /// Converts Result<TValue> to ActionResult<TValue>
        /// </summary>
        /// <param name="result">Result instance</param>
        public static implicit operator ActionResult<TValue>(Result<TValue> result) => result.ToActionResult();
        /// <summary>
        /// Converts TValue to Result
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Result<TValue>(TValue value) => new(value);
        /// <summary>
        /// Converts Error to Result
        /// </summary>
        /// <param name="error"></param>
        public static implicit operator Result<TValue>(Error error) => new(error);

        /// <summary>
        /// Creates Result<TValue> without error
        /// </summary>
        /// <param name="value">Result value</param>
        /// <returns>Result withot error</returns>
        public static Result<TValue> Ok(TValue value) => new(value);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="success"></param>
        /// <param name="failure"></param>
        /// <returns></returns>
        public TResult Match<TResult>(Func<TValue, TResult> success, Func<Error, TResult> failure)
            => !IsError ? success(Value!) : failure(_error!);

        /// <summary>
        /// Tries to get result value
        /// </summary>
        /// <param name="value">Result value</param>
        /// <returns>True if value is returned, otherwise false</returns>
        public bool TryGetValue([MaybeNullWhen(false)] out TValue value)
        {
            if (IsSuccess)
            {
                value = Value!;
                return true;
            }
            value = default;
            return false;
        }
    }
}
