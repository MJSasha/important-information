using ImpInfCommon.ApiServices;
using ImpInfCommon.Data.Models;
using ImpInfCommon.Data.Other;
using ImpInfCommon.Interfaces;
using ImpInfFrontCommon.Services;
using Microsoft.AspNetCore.Components;

namespace ImpInfFrontCommon.Pages
{
    public partial class Login : ComponentBase
    {
        [Inject]
        public CookieService CookieService { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected IAuth AuthService { get; set; }

        [Inject]
        protected ErrorsHandler ErrorsHandler { get; set; }

        protected AuthModel AuthModel { get; set; } = new();

        protected bool IsLoginFailed { get; set; } = false;


        protected async Task LoginAsync()
        {
            var user = new User();
            await ErrorsHandler.SaveExecute(async () => user = await AuthService.Login(AuthModel));

            if (user != null)
            {
                var claim = new UserClaim
                {
                    Name = AuthModel.Login,
                    Token = user.Token
                };
                await CookieService.SetCookies("token", claim.Token);
                NavigationManager.NavigateTo("/", true);
            }
            else
            {
                IsLoginFailed = true;
            }
        }

        protected class UserClaim
        {
            public string Name { get; set; }
            public string Token { get; set; }
        }
    }
}
