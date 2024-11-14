using PassportApplication.Errors.Enums;

namespace PassportApplication.Errors
{
    public class Error
    {
        public ErrorType ErrorType { get; set; }
        public string? Message { get; set; }

        public Error() 
        {
            ErrorType = ErrorType.None;
        }
        public Error(ErrorType errorType, string message)
        {
            ErrorType = errorType;
            Message = message;
        }
    }
}
