using ImpInfCommon.Data.Models;
using ImpInfCommon.Data.Other;
using ImpInfCommon.Interfaces;
using ImpInfFrontCommon.Services;
using Microsoft.AspNetCore.Components;

namespace ImpInfFrontCommon.Pages
{
    public partial class Scheldue
    {
        [Inject]
        private IDays DaysServices { get; set; }

        [Inject]
        private ErrorsHandler errorsHandler { get; set; }

        private List<Day> Days { get; set; } = new();
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

        protected override async Task OnInitializedAsync()
        {
            RefreshDays();
        }

        private async void RefreshDays()
        {
            var delta = DayOfWeek.Monday - StartDate.Value.Date.DayOfWeek;
            DateTime weekStartDate = StartDate.Value.Date.AddDays(delta == 1 ? -6 : delta);
            DateTime weekEndDate = weekStartDate.AddDays(6);

            await errorsHandler.SaveExecute(async () =>
                Days = (await DaysServices.GetByDates(new StartEndTime { Start = weekStartDate, End = weekEndDate })).OrderBy(d => d.Date).ToList()
            );

            StateHasChanged();
        }
    }
}
