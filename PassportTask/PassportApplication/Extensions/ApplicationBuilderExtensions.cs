using PassportApplication.Exceptions.Middleware;

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
