using System.Diagnostics;
using System.Net;
using System.Reflection.Metadata;
using System.Reflection;
using System.Text;

namespace PassportApplication.Errors
{
    public class Error
    {
        public Error(string message)
        {
            Message = message;
            StackTrace = GetStackTrace();
        }

        public Error(string message, Error innerError) : this(message)
        {
            InnerError = innerError;
        }

        public Error(string message, string stackTrace) : this(message)
        {
            StackTrace = stackTrace;
        }

        public Error(string message, HttpStatusCode statusCode) : this(message)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCode? StatusCode { get; }
        public Error? InnerError { get; }
        public string Message { get; }
        public string? StackTrace { get; }

        public static implicit operator string(Error error) => error.ToString();

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
