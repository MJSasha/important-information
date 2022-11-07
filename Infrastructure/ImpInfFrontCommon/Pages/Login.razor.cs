using ImpInfCommon.ApiServices;
using ImpInfCommon.Data.Models;
using ImpInfCommon.Data.Other;
using ImpInfFrontCommon.Services;
using Microsoft.AspNetCore.Components;
using TgBotLib.Exceptions;

namespace ImpInfFrontCommon.Pages
{
    public partial class Login : ComponentBase
    {
        [CascadingParameter]
        public User CurrentUser { get; set; }

        [Inject]
        public CookieService CookieService { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected AuthService AuthService { get; set; }

        [Inject]
        protected ErrorsHandler ErrorsHandler { get; set; }

        protected AuthModel AuthModel { get; set; } = new();

        protected bool IsLoginFailed { get; set; } = false;


        protected async Task LoginAsync()
        {
            try
            {
                var CurrentUser = await AuthService.Login(AuthModel);
                var claim = new UserClaim
                {
                    Name = AuthModel.Login,
                    Token = CurrentUser.Token
                };
                await CookieService.SetCookies("token", claim.Token);
                NavigationManager.NavigateTo("/", true);
            }
            catch (Exception ex) when (ex is ErrorResponseException errorResponse && errorResponse.StatusCode != System.Net.HttpStatusCode.Unauthorized)
            {
                IsLoginFailed = true;
            }
            catch (Exception ex)
            {
                ErrorsHandler.ProcessError(ex);
            }
        }

        protected class UserClaim
        {
            public string Name { get; set; }
            public string Token { get; set; }
        }
    }
}
