using BethanysPieShop.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace BethanysPieShop.App.Services
{
    public class CountryDataService : ICountryDataService
    {
        private readonly HttpClient _httpClient;

        public CountryDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Country>> GetAllCountries() => await _httpClient.GetFromJsonAsync<IEnumerable<Country>>($"api/country", new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        public async Task<Country> GetCountryById(int countryId) => await _httpClient.GetFromJsonAsync<Country>($"api/country{countryId}", new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
    }
}
