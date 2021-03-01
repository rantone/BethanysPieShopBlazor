using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BethanysPieShop.App.Services;
using BethanysPieShop.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace BethanysPieShop.App.Pages
{
    public partial class EmployeeOverviewVirtual
    {
        private IList<Employee> _employees = new List<Employee>();

        [Inject]
        private IEmployeeDataService _employeeDataService { get; set; }
        private float itemHeight = 50;
        private int totalNumberOfEmployees = 1000;

        protected async override Task OnInitializedAsync() => _employees = (await _employeeDataService.GetLongEmployeeList()).ToList();

        private async ValueTask<ItemsProviderResult<Employee>> LoadEmployees(ItemsProviderRequest request)
        {
            //assume we have asked the api for the total in a seperate call
            var numberOfEmployees = Math.Min(request.Count, totalNumberOfEmployees - request.StartIndex);
            var employees = await _employeeDataService.GetTakeLongEmployeeList(request.StartIndex, numberOfEmployees);
            return new ItemsProviderResult<Employee>(employees, totalNumberOfEmployees);
        }
    }
}