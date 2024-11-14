using System.Diagnostics;

namespace PassportApplication.Exceptions.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

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
