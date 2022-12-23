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
        protected IAuthService AuthService { get; set; }

        [Inject]
        protected IErrorsHandler ErrorsHandler { get; set; }

        protected AuthModel AuthModel { get; set; } = new();

        protected bool IsLoginFailed { get; set; } = false;


        protected async Task LoginAsync()
        {
            try
            {
                var user = await AuthService.Login(AuthModel);

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
