using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

using PassportApplication.Errors;
using PassportApplication.Extensions;

namespace PassportApplication.Results.Generic
{
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
        public TValue? Value { get; }
        public static implicit operator ActionResult<TValue>(Result<TValue> result) => result.ToActionResult();

        public static implicit operator Result<TValue>(TValue value) => new(value);
        public static implicit operator Result<TValue>(Error error) => new(error);
        public static Result<TValue> Ok(TValue value) => new(value);

        public TResult Match<TResult>(Func<TValue, TResult> success, Func<Error, TResult> failure)
            => !IsError ? success(Value!) : failure(_error!);

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
