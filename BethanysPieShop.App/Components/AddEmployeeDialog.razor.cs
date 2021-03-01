using System;
using System.Threading.Tasks;
using BethanysPieShop.App.Services;
using BethanysPieShop.Shared;
using Microsoft.AspNetCore.Components;

namespace BethanysPieShop.App.Components
{
    public partial class AddEmployeeDialog
    {
        private Employee Employee { get; set; } = new Employee { CountryId = 1, JobCategoryId = 1, BirthDate = DateTime.Now, JoinedDate = DateTime.Now };

        [Inject]
        private IEmployeeDataService _employeeDataService { get; set; }

        [Parameter]
        public EventCallback<bool> CloseEnventCallback { get; set; }

        private bool _showDialog;

        public void Show()
        {
            ResetDialog();
            _showDialog = true;
            StateHasChanged();
        }

        public void Close()
        {
            _showDialog = false;
            StateHasChanged();
        }

        protected async Task HandleValidSubmit()
        {
            var addedEmployee = await _employeeDataService.AddEmployee(Employee);
            Close();
            await CloseEnventCallback.InvokeAsync(true);
        }

        private void ResetDialog() => Employee = new Employee { CountryId = 1, JobCategoryId = 1, BirthDate = DateTime.Now, JoinedDate = DateTime.Now};
    }
}