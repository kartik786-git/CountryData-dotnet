using CountryData.Standard;
using Newtonsoft.Json;
using WebApi.Interface;

namespace WebApi.Services
{
    public class CountryDataServices : ICountryDataServices, IDisposable
    {
        private readonly CountryHelper _countryHelper;
        private bool _disposed = false;
        public CountryDataServices(CountryHelper countryHelper)
        {
            _countryHelper = countryHelper;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources here.
                }

                // Dispose unmanaged resources here.

                _disposed = true;
            }
        }

        public Task<string> GetCountriesAsync()
        {
            var countries = _countryHelper.GetCountries();

            if (countries == null)
            {
                return Task.FromResult("No countries found");
            }

            return Task.FromResult(JsonConvert.SerializeObject(countries, Newtonsoft.Json.Formatting.Indented));
        }

        public Task<string> GetCountryByCodeAsync(string code)
        {
            var country = _countryHelper.GetCountryByCode(code);
            if (country == null)
            {
                return Task.FromResult("Country not found");
            }

            return Task.FromResult(JsonConvert.SerializeObject(country, Newtonsoft.Json.Formatting.Indented));
        }

        public Task<string> GetCountryByPhoneCodeAsync(string phoneCode)
        {
            var country = _countryHelper.GetCountryByPhoneCode(phoneCode);
            if (country == null)
            {
                return Task.FromResult("Country not found");
            }

            return Task.FromResult(JsonConvert.SerializeObject(country, Newtonsoft.Json.Formatting.Indented));
        }

        public Task<string> GetCountryDataAsync(int offset = 1, int limit = 10, string? searchQuery = null)
        {
            var countries = _countryHelper.GetCountryData();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                searchQuery = searchQuery.ToLower();
                countries = countries.Where(x =>
                    x.CountryName.ToLower().Contains(searchQuery)
                ).ToList();
            }
            var paginatedCountries = countries.Skip((offset - 1) * limit).Take(limit).ToList();

            if (paginatedCountries == null || !paginatedCountries.Any())
            {
                return Task.FromResult("No countries found");
            }

            return Task.FromResult(JsonConvert.SerializeObject(paginatedCountries, Newtonsoft.Json.Formatting.Indented));

        }

        public Task<string> GetCountryFlagAsync(string countryCode)
        {
            var flag = _countryHelper.GetCountryEmojiFlag(countryCode);
            if (flag == null)
            {
                return Task.FromResult("Flag not found");
            }

            return Task.FromResult(flag);
        }

        public Task<string> GetPhoneCodeByCountryShortCodeAsync(string countryCode)
        {
            var phoneCode = _countryHelper.GetPhoneCodeByCountryShortCode(countryCode);
            if (phoneCode == null)
            {
                return Task.FromResult("Phone code not found");
            }

            return Task.FromResult(phoneCode);
        }

        public Task<string> GetRegionsByCountryCodeAsync(string countryCode)
        {
            var regions = _countryHelper.GetRegionByCountryCode(countryCode);
            if (regions == null)
            {
                return Task.FromResult("No regions found");
            }

            return Task.FromResult(JsonConvert.SerializeObject(regions, Newtonsoft.Json.Formatting.Indented));
        }
    }
}
