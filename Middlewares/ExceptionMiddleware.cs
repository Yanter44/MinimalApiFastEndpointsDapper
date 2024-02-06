using Serilog;
namespace MinimalApi_test____Dapper___PostgreSQL.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                Log.Error(ex, "Fuck shit something wrong");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("An unexpected error occurred. Please try again later. P.S. Eblan neobrazovaniy");
            }
        }
    }
}
