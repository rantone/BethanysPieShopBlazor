using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BethanysPieShop.App.Services;
using BethanysPieShop.ComponentsLibrary.Map;
using BethanysPieShop.Shared;
using Microsoft.AspNetCore.Components;

namespace BethanysPieShop.App.Pages
{
    public partial class EmployeeDetail
    {
        [Parameter]
        public int EmployeeId { get; set; }

        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }

        private Employee Employee { get; set; } = new Employee();

        private List<Marker> MapMarkers { get; set; } = new List<Marker>();
        protected override async Task OnInitializedAsync()
        {
            Employee = await EmployeeDataService.GetEmployeeDetails(EmployeeId);
            MapMarkers = new List<Marker>
            {
                new Marker{Description = $"{Employee.FirstName} {Employee.LastName}",  ShowPopup = false, X = Employee.Longitude, Y = Employee.Latitude}
            };
        }
    }
}