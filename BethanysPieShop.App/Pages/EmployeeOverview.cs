using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BethanysPieShop.App.Components;
using BethanysPieShop.App.Services;
using BethanysPieShop.Shared;
using Microsoft.AspNetCore.Components;

namespace BethanysPieShop.App.Pages
{
    public partial class EmployeeOverview
    {
        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }
        private IEnumerable<Employee> Employees { get; set; }

        protected AddEmployeeDialog AddEmployeeDialog { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Employees = (await EmployeeDataService.GetAllEmployees()).ToList();
        }

        protected void QuickAddEmployee() => AddEmployeeDialog.Show();

        public async void AddEmployeeDialog_OnDialogClose()
        {
            Employees = await EmployeeDataService.GetAllEmployees();
            StateHasChanged();
        }

    }
}