using System.Net;
using PassportApplication.Errors;

namespace PassportApplication.Exceptions.Dto
{
    public class ExceptionDto
    {
        public HttpStatusCode? StatusCode { get; }
        public string Message { get; }

        public ExceptionDto(Error error) 
        { 
            StatusCode = error.StatusCode;
            Message = error.Message;
        }
    }
}
