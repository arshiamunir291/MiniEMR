using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

namespace MiniEMR.Middlewares
{
    public class CustomAuthorizationMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value?.ToLower();

            if (
                context.Request.Method == HttpMethods.Options ||
                string.IsNullOrEmpty(path) ||
                path == "/" ||
                path == "/swagger" ||
                path == "/swagger/index.html" ||
                (path != null && path.StartsWith("/swagger/")) ||
                path == "/api/auth/login" ||
                path == "/favicon.ico"
            )
            {
                await next(context);
                return;
            }

            var endpoint = context.GetEndpoint();

            if (endpoint?.Metadata.GetMetadata<AllowAnonymousAttribute>() != null)
            {
                await next(context);
                return;
            }

            if (context.User?.Identity == null ||
                !context.User.Identity.IsAuthenticated)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(new
                    {
                        Message = "Unauthorized access."
                    }));

                return;
            }

            var userIdClaim =
                context.User.FindFirst("userId")?.Value
                ?? context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userIdClaim) ||
                !int.TryParse(userIdClaim, out _))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(new
                    {
                        Message = "Invalid token."
                    }));

                return;
            }

            await next(context);
        }
    }
}