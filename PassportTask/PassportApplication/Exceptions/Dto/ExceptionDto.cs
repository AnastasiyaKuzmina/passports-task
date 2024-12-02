using System.Net;
using PassportApplication.Errors;

namespace PassportApplication.Exceptions.Dto
{
    /// <summary>
    /// Exception DTO
    /// </summary>
    public record ExceptionDto
    {
        /// <summary>
        /// Status code
        /// </summary>
        public HttpStatusCode? StatusCode { get; init; }
        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; init; }

        /// <summary>
        /// Constructor of ExceptionDto
        /// </summary>
        /// <param name="error">Error instance</param>
        public ExceptionDto(Error error) 
        { 
            StatusCode = error.StatusCode;
            Message = error.Message;
        }
    }
}
