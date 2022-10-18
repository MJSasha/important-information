using ImpInfCommon.ApiServices;
using ImpInfCommon.Data.Other;
using ImpInfFrontCommon.Services;
using Microsoft.AspNetCore.Components;


namespace ImpInfFrontCommon.Pages
{
    public partial class Login : ComponentBase
    {
        public Login()
        {
            AuthModel = new AuthModel();
        }

        [Inject]
        public CookieService CookieService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public AuthService AuthService { get; set; }

        public AuthModel AuthModel { get; set; }

        public bool IsBad { get; set; } = false; // такой кринж


        protected async Task LoginAsync()
        {
            try
            {
                var claim = new UserClaim
                {
                    Name = AuthModel.Login,
                    Token = await AuthService.Login(AuthModel)
                };
                await CookieService.WriteCookies("token", claim.Token);
                NavigationManager.NavigateTo("/");
            }
            catch (Exception)
            {
                IsBad = true;
                throw;
            }
        }

        public class UserClaim
        {
            public string Name { get; set; }
            public string Token { get; set; }
        }
    }
}
