using ImpInfCommon.Data.Models;
using ImpInfCommon.Interfaces;
using ImpInfFrontCommon.Services;
using Microsoft.AspNetCore.Components;

namespace ImpInfFrontCommon.Pages
{
    public partial class LessonsPage : ComponentBase
    {
        [Inject]
        public ILessonService LessonService { get; set; }

        [Inject]
        public ErrorsHandler ErrorsHandler { get; set; }

        private List<Lesson> lessons = new();

        protected override async Task OnInitializedAsync()
        {
            await ErrorsHandler.SaveExecute(async () => lessons = await LessonService.Get());
        }
    }
}
