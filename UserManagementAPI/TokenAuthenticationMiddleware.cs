namespace UserManagementAPI
{
    public class TokenAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private const string AUTH_HEADER = "Authorization";
        private const string VALID_TOKEN = "123"; // Replace with secure config

        public TokenAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue(AUTH_HEADER, out var tokenValue) &&
                string.Equals(tokenValue.ToString().Trim(), VALID_TOKEN, StringComparison.Ordinal))
            {
                await _next(context);
                return;
            }

            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            var response = new { error = "Unauthorized" };
            await context.Response.WriteAsJsonAsync(response);
        }
    }

}
