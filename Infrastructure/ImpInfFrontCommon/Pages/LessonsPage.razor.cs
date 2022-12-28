using ImpInfCommon.Data.Models;
using ImpInfCommon.Interfaces;
using ImpInfFrontCommon.Components.Dialogs.LessonRedactionDialog;
using ImpInfFrontCommon.Services;
using Microsoft.AspNetCore.Components;

namespace ImpInfFrontCommon.Pages
{
    public partial class LessonsPage : ComponentBase
    {
        [Inject]
        public ILessonService LessonService { get; set; }

        [Inject]
        public DialogService DialogService { get; set; }

        private List<Lesson> lessons = new();

        protected override async Task OnInitializedAsync()
        {
            await RefreshLessons();
        }

        private async Task EditLesson(Lesson lesson)
        {
            try
            {
                var newLesson = await DialogService.Show<LessonRedactionDialog, LessonRedactionDialogParams, Lesson>(new LessonRedactionDialogParams { Lesson = lesson });
                if (newLesson != null) await LessonService.Patch(newLesson.Id, newLesson);
                await RefreshLessons();
            }
            catch { /*ignore*/ }
        }

        private async Task RefreshLessons() => lessons = await LessonService.Get();
    }
}
