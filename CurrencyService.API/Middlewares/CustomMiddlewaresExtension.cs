using Microsoft.AspNetCore.Builder;

namespace CurrencyService.API.Middlewares
{
    public static class CustomMiddlewaresExtension
    {
        public static IApplicationBuilder UseCustomAuthorizationMiddleware(this IApplicationBuilder builder) =>
            builder.UseMiddleware<TokenMiddleware>();
    }
}
