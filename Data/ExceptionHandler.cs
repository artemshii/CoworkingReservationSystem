using Microsoft.EntityFrameworkCore;

namespace CoworkingReservation.Data
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;
        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await httpContext.Response.WriteAsJsonAsync("Error");
            }

        }
    }
}
