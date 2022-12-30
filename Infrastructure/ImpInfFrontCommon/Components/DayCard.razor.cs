using ImpInfCommon.Data.Definitions;
using ImpInfCommon.Data.Models;
using Microsoft.AspNetCore.Components;

namespace ImpInfFrontCommon.Components
{
    public partial class DayCard : ComponentBase
    {
        [Parameter]
        public Day Day { get; set; }

        private List<Note> notes;

        private string GetImporatanceColor(Importance importance)
        {
            return importance switch
            {
                Importance.VeryImportant => "--veryimportant-color",
                Importance.Important => "--important-color",
                Importance.NotImportant => "--notimportant-color",
                _ => ""
            };
        }
    }
}
