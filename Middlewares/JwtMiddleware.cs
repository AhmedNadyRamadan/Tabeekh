namespace Tabeekh.Middlewares
{
    public class JwtMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var token = context.Request.Cookies["X-Access-Token"];
            if (!string.IsNullOrEmpty(token))
            {
                context.Response.Headers.Append("Authorization", $"Bearer {token}");
            }
            await next(context); 
        }
    }
}