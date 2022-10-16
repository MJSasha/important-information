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

        private List<Day> Days
        {
            get => days;
            set
            {
                days = value;
                StateHasChanged();
            }
        }
        private DateTimeOffset? StartDate
        {
            get => startDate;
            set
            {
                startDate = value;
                RefreshDays();
            }
        }

        private DateTimeOffset? startDate = DateTime.Now;
        private List<Day> days = new();

        protected override async Task OnInitializedAsync()
        {
            RefreshDays();
        }

        private async void RefreshDays()
        {
            var delta = DayOfWeek.Monday - StartDate.Value.Date.DayOfWeek - 1;
            DateTime weekStartDate = StartDate.Value.Date.AddDays(delta);
            DateTime weekEndDate = weekStartDate.AddDays(8);

            Days = (await DaysServices.Get(new StartEndTime { Start = weekStartDate, End = weekEndDate })).OrderBy(d => d.Date.Day).ToList();
        }
    }
}
