using CurrencyService.Application.Features.FindRaports;
using CurrencyService.Domain.ApiModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CurrencyService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly IFindRaportsForCurrency _findRaportsForCurrency;

        public CurrencyController(IFindRaportsForCurrency findRaportsForCurrency)
        {
            _findRaportsForCurrency = findRaportsForCurrency;
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get([FromBody] CurrencyRequestApiModel currencyRequest)
        {
            if (currencyRequest.EndDate < currencyRequest.StartDate)
                currencyRequest.EndDate = currencyRequest.StartDate;

            if (currencyRequest.StartDate > DateTime.Now.Date)
                return NotFound($"Start date cannot be later then current day. Actual value: {currencyRequest.StartDate.ToString("dd-MM-yyyy")}.");

            var output = await _findRaportsForCurrency.FindRaports(currencyRequest.CurrencyCodes, currencyRequest.StartDate, currencyRequest.EndDate);

            return Ok(output);
        }
    }
}
