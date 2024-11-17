using System.Net;
using PassportApplication.Errors;

namespace PassportApplication.Exceptions.Dto
{
    /// <summary>
    /// Exception DTO
    /// </summary>
    public class ExceptionDto
    {
        /// <summary>
        /// Status code
        /// </summary>
        public HttpStatusCode? StatusCode { get; }
        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; }

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
