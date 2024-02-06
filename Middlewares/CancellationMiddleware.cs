namespace MinimalApi_test____Dapper___PostgreSQL.Middlewares
{
    public class CancellationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CancellationMiddleware> _logger;
        public CancellationMiddleware(RequestDelegate next, ILogger<CancellationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception _) when (_ is OperationCanceledException or TaskCanceledException)
            {
                Console.WriteLine($"Task cancelled");
            }

        }
    }
}
