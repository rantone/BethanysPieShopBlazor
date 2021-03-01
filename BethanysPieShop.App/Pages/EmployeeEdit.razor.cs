using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BethanysPieShop.App.Services;
using BethanysPieShop.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BethanysPieShop.App.Pages
{
    public partial class EmployeeEdit
    {
        [Inject]
        private IEmployeeDataService EmployeeDataService { get; set; }

        [Inject]
        private ICountryDataService CountryDataService { get; set; }

        [Inject]
        private IJobCategoryDataService JobCategoryDataService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Parameter]
        public int EmployeeId { get; set; }

        public Employee Employee { get; set; } = new Employee();
        public IEnumerable<Country> Countries { get; set; } = new List<Country>();
        public IEnumerable<JobCategory> JobCategories { get; set; } = new List<JobCategory>();

        //Used to store state of screen
        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool Saved;

        private IReadOnlyList<IBrowserFile> selectedFiles;
        private ElementReference LastNameInput;

        protected override async Task OnInitializedAsync()
        {
            Saved = false;
            Employee = EmployeeId == 0
                ?new Employee {CountryId = 1, JobCategoryId = 1, BirthDate = DateTime.Now, JoinedDate = DateTime.Now}
                :await EmployeeDataService.GetEmployeeDetails(EmployeeId);

            Countries = await CountryDataService.GetAllCountries();
            JobCategories = await JobCategoryDataService.GetAllJobCategories();
        }

        protected async override Task OnAfterRenderAsync(bool firstRender) => await LastNameInput.FocusAsync();

        protected async Task HandelValidSubmit()
        {
            if(Employee.EmployeeId == 0)
            {
                if(selectedFiles != null)
                {
                    var file = selectedFiles[0];
                    using var stream = file.OpenReadStream();
                    using var ms = new MemoryStream();
                    await stream.CopyToAsync(ms);
                    stream.Close();

                    Employee.ImageName = file.Name;
                    Employee.ImageContent = ms.ToArray();
                }

                var addedEmployee = await EmployeeDataService.AddEmployee(Employee);
                if(addedEmployee != null)
                {
                    StatusClass = "alert-success";
                    Message = "Employee updated successfully.";
                    Saved = true;
                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong added th new employee. Please try again.";
                    Saved = false;
                }
            }
            else
            {
                await EmployeeDataService.UpdateEmployee(Employee);
                StatusClass = "alert-success";
                Message = "Employee updated successfully.";
                Saved = true;
            }
        }
        protected void HandelInvalidSubmit()
        {
            StatusClass = "alert-danger";
            Message = "Something are some validation errors. Please try again.";
        }

        protected async Task DeleteEmployee()
        {
            await EmployeeDataService.DeleteEmployee(Employee.EmployeeId);

            StatusClass = "alert-success";
            Message = "Deleted successfully";
            Saved = true;
        }

        protected void NavigateToOverView() => NavigationManager.NavigateTo("/employeeoverview");

        private void OnInputFileChange(InputFileChangeEventArgs e)
        {
            selectedFiles = e.GetMultipleFiles();
            Message = $"{selectedFiles.Count} file(s) selected";
            StateHasChanged();
        }
    }
}