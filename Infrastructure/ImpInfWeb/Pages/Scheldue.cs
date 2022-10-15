using ImpInfCommon.ApiServices;
using ImpInfCommon.Data.Models;
using ImpInfCommon.Data.Other;
using Microsoft.AspNetCore.Components;

namespace ImpInfWeb.Pages
{
    public partial class Scheldue
    {
        [Inject]
        private DaysServices DaysServices { get; set; }

        private List<Day> Days { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            DateTime weekStartDate = DateTime.Now.AddDays(-(DateTime.Now.DayOfWeek - DayOfWeek.Monday));
            DateTime weekEndDate = weekStartDate.AddDays(6);

            Days = await DaysServices.Get(new StartEndTime { Start = weekStartDate, End = weekEndDate });
        }
    }
}
