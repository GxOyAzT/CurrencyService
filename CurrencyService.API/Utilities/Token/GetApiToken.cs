using Microsoft.Extensions.Configuration;

namespace CurrencyService.API.Utilities.Token
{
    public class GetApiToken : IGetApiToken
    {
        private readonly IConfiguration _configuration;

        public GetApiToken(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Get()
        {
            return _configuration["Token"];
        }
    }
}
