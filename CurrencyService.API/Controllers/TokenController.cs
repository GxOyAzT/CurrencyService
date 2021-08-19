using CurrencyService.API.Utilities.Token;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly IGetApiToken _getApiToken;

        public TokenController(IGetApiToken getApiToken)
        {
            _getApiToken = getApiToken;
        }

        [HttpGet]
        [Route("generate")]
        public IActionResult GetToken()
        {
            return Ok(_getApiToken.Get());
        }
    }
}
