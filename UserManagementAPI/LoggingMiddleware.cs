namespace UserManagementAPI
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            // Log incoming request
            var method = context.Request.Method;
            var path = context.Request.Path;

            await _next(context);

            // Log outgoing response
            var statusCode = context.Response.StatusCode;
            _logger.LogInformation("Request: {Method} {Path} - Response: {StatusCode}", method, path, statusCode);
        }
    }

}
