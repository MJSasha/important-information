using ImpInfCommon.ApiServices;
using ImpInfCommon.Data.Other;
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
        protected AuthService AuthService { get; set; }

        protected AuthModel AuthModel { get; set; } = new();

        protected bool IsBad { get; set; } = false; // такой кринж


        protected async Task LoginAsync()
        {
            try
            {
                var claim = new UserClaim
                {
                    Name = AuthModel.Login,
                    Token = await AuthService.Login(AuthModel)
                };
                await CookieService.SetCookies("token", claim.Token);
                NavigationManager.NavigateTo("/", true);
            }
            catch (Exception)
            {
                IsBad = true;
                throw;
            }
        }

        protected class UserClaim
        {
            public string Name { get; set; }
            public string Token { get; set; }
        }
    }
}
