using ImpInfCommon.Interfaces;
using Microsoft.AspNetCore.Components;

namespace ImpInfFrontCommon.Services
{
    public class ErrorsHandler : IErrorsHandler
    {
        protected NavigationManager NavigationManager { get; set; }
        protected DialogService DialogService { get; set; }

        public ErrorsHandler(NavigationManager navigationManager, DialogService dialogService)
        {
            NavigationManager = navigationManager;
            DialogService = dialogService;
        }

        public async void ProcessError(Exception ex)
        {
            //if (ex is ErrorResponseException errorResponse)
            //{
            //    if (errorResponse.StatusCode == HttpStatusCode.Unauthorized) NavigationManager.NavigateTo(PagesRouts.Logout);
            //}
            //else await DialogService.Show<MessageDialog, MessageDialogParams, object>(new MessageDialogParams("Упс...", "Произошла ошибочка"));
        }

        public async Task SaveExecute(Func<Task> action)
        {
            try
            {
                await action();
            }
            catch (Exception ex)
            {
                ProcessError(ex);
            }
        }
    }
}
