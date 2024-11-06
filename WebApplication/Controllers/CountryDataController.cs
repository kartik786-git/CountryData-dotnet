using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interface;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryDataController : ControllerBase
    {
        private readonly ICountryDataServices _countryDataServices;

        public CountryDataController(ICountryDataServices countryDataServices)
        {
            _countryDataServices = countryDataServices;
        }

        [HttpGet]
        [Route("countries")]
        public async Task<IActionResult> GetCountriesAsync()
        {
            var countries = await _countryDataServices.GetCountriesAsync();
            return Ok(countries);
        }

        [HttpGet]
        [Route("country/{code}")]
        public async Task<IActionResult> GetCountryByCodeAsync(string code)
        {
            var country = await _countryDataServices.GetCountryByCodeAsync(code);
            return Ok(country);
        }

        [HttpGet]
        [Route("countrydata")]
        public async Task<IActionResult> GetCountryDataAsync([FromQuery] int offset = 1, [FromQuery] int limit = 10, string? searchQuery = null)
        {
            var countryData = await _countryDataServices.GetCountryDataAsync(offset, limit, searchQuery);
            return Ok(countryData);
        }

        [HttpGet]
        [Route("regions/{countryCode}")]
        public async Task<IActionResult> GetRegionsByCountryCodeAsync(string countryCode)
        {
            var regions = await _countryDataServices.GetRegionsByCountryCodeAsync(countryCode);
            return Ok(regions);
        }

        [HttpGet]
        [Route("flag/{countryCode}")]
        public async Task<IActionResult> GetCountryFlagAsync(string countryCode)
        {
            var flag = await _countryDataServices.GetCountryFlagAsync(countryCode);
            return Ok(flag);
        }

        [HttpGet]
        [Route("phonecode/{countryCode}")]
        public async Task<IActionResult> GetPhoneCodeByCountryShortCodeAsync(string countryCode)
        {
            var phoneCode = await _countryDataServices.GetPhoneCodeByCountryShortCodeAsync(countryCode);
            return Ok(phoneCode);
        }

        [HttpGet]
        [Route("countrybyphonecode/{phoneCode}")]
        public async Task<IActionResult> GetCountryByPhoneCodeAsync(string phoneCode)
        {
            var country = await _countryDataServices.GetCountryByPhoneCodeAsync(phoneCode);
            return Ok(country);
        }
    }
}
