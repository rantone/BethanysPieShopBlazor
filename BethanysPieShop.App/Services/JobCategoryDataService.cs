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
    public class JobCategoryDataService : IJobCategoryDataService
    {
        private readonly HttpClient _httpClient;

        public JobCategoryDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<JobCategory>> GetAllJobCategories() => await _httpClient.GetFromJsonAsync<IEnumerable<JobCategory>>($"api/jobcategory", new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        public async Task<JobCategory> GetJobCategoryById(int jobCategoryId) => await _httpClient.GetFromJsonAsync<JobCategory>($"api/jobcategory/{jobCategoryId}", new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
    }
}
