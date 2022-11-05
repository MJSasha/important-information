using ImpInfCommon.Exceptions;
using ImpInfFrontCommon.Definitions;
using Microsoft.AspNetCore.Components;
using System.Net;
using TgBotLib.Exceptions;

namespace ImpInfFrontCommon.Services
{
    public class ErrorsHandler
    {
        protected NavigationManager NavigationManager { get; set; }

        public ErrorsHandler(NavigationManager navigationManager)
        {
            NavigationManager = navigationManager;
        }

        public void ProcessError(Exception ex)
        {
            if (ex is ErrorResponseException errorResponse)
            {
                if (errorResponse.StatusCode == HttpStatusCode.Unauthorized) NavigationManager.NavigateTo(PagesRouts.Logout);
                // Тут вкорячим обработку других исключений, а в самом конце модалку с "Что-то пошло не так"
            }
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
