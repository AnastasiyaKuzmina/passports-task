using System.Diagnostics;
using System.Net;
using System.Reflection.Metadata;
using System.Reflection;
using System.Text;

namespace PassportApplication.Errors
{
    /// <summary>
    /// Error class
    /// </summary>
    public class Error
    {
        /// <summary>
        /// Constructor of Error
        /// </summary>
        /// <param name="message">Error message</param>
        public Error(string message)
        {
            Message = message;
            StackTrace = GetStackTrace();
        }

        /// <summary>
        /// Constructor of Error
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="innerError">Inner error</param>
        public Error(string message, Error innerError) : this(message)
        {
            InnerError = innerError;
        }

        /// <summary>
        /// Constructor of Error
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="stackTrace">Stack trace</param>
        public Error(string message, string stackTrace) : this(message)
        {
            StackTrace = stackTrace;
        }

        /// <summary>
        /// Constructor of Error
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="statusCode">Status code</param>
        public Error(string message, HttpStatusCode statusCode) : this(message)
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// Status code
        /// </summary>
        public HttpStatusCode? StatusCode { get; }
        /// <summary>
        /// Inner error
        /// </summary>
        public Error? InnerError { get; }
        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; }
        /// <summary>
        /// Stack trace
        /// </summary>
        public string? StackTrace { get; }

        /// <summary>
        /// Converts Error to string
        /// </summary>
        /// <param name="error">Error instance</param>
        public static implicit operator string(Error error) => error.ToString();

        /// <summary>
        /// Creates information string about error
        /// </summary>
        /// <returns>Error info</returns>
        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(GetType().Name);
            stringBuilder.Append(" { ");
            if (PrintMembers(stringBuilder))
                stringBuilder.Append(' ');
            stringBuilder.Append('}');
            return stringBuilder.ToString();
        }
        protected bool PrintMembers(StringBuilder builder)
        {
            builder.Append("InnerError = ");
            builder.Append(InnerError is null ? "null" : $"\n{InnerError}");
            builder.Append(", Message = ");
            builder.Append(Message);
            builder.Append(", StackTrace = \n");
            builder.Append(StackTrace ?? "null");
            return true;
        }

        private static string GetStackTrace()
        {
            var stackTrace = new StackTrace(skipFrames: 2);
            var stringBuilder = new StringBuilder();

            foreach (var stackFrame in stackTrace.GetFrames().Reverse())
                if (stackFrame.GetMethod()?.DeclaringType?.Assembly.GetName() == new AssemblyReference().GetAssemblyName())
                {
                    stringBuilder.Append($"\tat {stackFrame.GetMethod()?.DeclaringType?.FullName}.{stackFrame.GetMethod()?.Name}");
                    var parameters = stackFrame.GetMethod()?.GetParameters() ?? Array.Empty<ParameterInfo>();
                    stringBuilder.Append($"({string.Join(", ", parameters.Select(p => p.Name))})\n");
                }

            return stringBuilder.ToString();
        }
    }
}
