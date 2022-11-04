using ImpInfCommon.ApiServices;
using ImpInfCommon.Data.Models;
using ImpInfCommon.Data.Other;
using ImpInfFrontCommon.Services;
using Microsoft.AspNetCore.Components;


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

        protected AuthModel AuthModel { get; set; } = new();

        protected bool IsLoginFailed { get; set; } = false; // такой кринж


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
            catch (Exception)
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
