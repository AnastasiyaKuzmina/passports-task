namespace PassportApplication.Exceptions.Middleware
{
    /// <summary>
    /// Exception handler middleware
    /// </summary>
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Constructor of ExceptionHandlerMiddleware
        /// </summary>
        /// <param name="next">Next middleware</param>
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invokes middleware
        /// </summary>
        /// <param name="context">HttpContext instance</param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (NotImplementedException ex)
            {
                await CallResponse(context, "Server Error: Not implemented method");
            }
            catch (Exception ex)
            {
                await CallResponse(context, "Server Error");
            }
        }

        private async Task CallResponse(HttpContext context, string message)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "text/plain";
            await context.Response.WriteAsync(message);
        }
    }
}
