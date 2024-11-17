using PassportApplication.Exceptions.Middleware;

namespace PassportApplication.Extensions
{
    /// <summary>
    /// Application builder extensions
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Adds ExceptionHandler middleware
        /// </summary>
        /// <param name="app">IApplicationBuilder instance</param>
        public static void UseExceptionHandlerMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
