using PassportApplication.Exceptions;

namespace PassportApplication.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseExceptionHandlerMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
