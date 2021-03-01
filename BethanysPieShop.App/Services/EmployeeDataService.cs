using BethanysPieShop.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Text.Json;

namespace BethanysPieShop.App.Services
{
    public class EmployeeDataService : IEmployeeDataService
    {
        private readonly HttpClient _httpClient;

        public EmployeeDataService(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<IEnumerable<Employee>> GetLongEmployeeList()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Employee>>($"api/employee/long");
        }

        public async Task<IEnumerable<Employee>> GetTakeLongEmployeeList(int startIndex, int count)
        {
            return await _httpClient.GetFromJsonAsync<List<Employee>>($"api/employee/long/{startIndex}/{count}");
        }
        public async Task<Employee> AddEmployee(Employee employee)
        {
            var response = await _httpClient.PostAsJsonAsync("api/employee", employee);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Employee>();
            }

            return null;
        }

        public async Task UpdateEmployee(Employee employee) => await _httpClient.PutAsJsonAsync("api/employee", employee);

        public async Task DeleteEmployee(int employeeId) => await _httpClient.DeleteAsync($"api/employee/{employeeId}");

        public async Task<IEnumerable<Employee>> GetAllEmployees() => await _httpClient.GetFromJsonAsync<IEnumerable<Employee>>($"api/employee", new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        public async Task<Employee> GetEmployeeDetails(int employeeId) => await _httpClient.GetFromJsonAsync<Employee>($"api/employee/{employeeId}", new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });


    }
}
