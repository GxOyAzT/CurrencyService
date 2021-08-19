using CurrencyService.API.Utilities.Token;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyService.API.Middlewares
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public TokenMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.ToString().ToLower() == "/api/token/generate")
            {
                await _next(context);
            }

            if (context.Request.Headers.ToList().FirstOrDefault(e => e.Key == "Token").Value == _configuration["Token"])
            {
                await _next(context);
            }
            else
            {
                context.Response.StatusCode = 403;
            }
        }
    }
}
