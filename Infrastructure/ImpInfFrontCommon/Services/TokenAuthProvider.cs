using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using static ImpInfFrontCommon.Pages.Login;

namespace ImpInfFrontCommon.Services
{

    public class TokenAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService localStorageService;

        public TokenAuthStateProvider(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {

            var userClaim = await localStorageService.GetAsync<UserClaim>("imp-inf");

            if (userClaim == null) return GetStateAnonymous();
            if (string.IsNullOrWhiteSpace(userClaim.Token)) return GetStateAnonymous();


            var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, userClaim.Name),
            new Claim(ClaimTypes.Authentication, userClaim.Token),
            new Claim(ClaimTypes.Role, "User")
        };

            var claimsIdentity = new ClaimsIdentity(claims, "Token");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            return new AuthenticationState(claimsPrincipal);
        }

        private static AuthenticationState GetStateAnonymous()
        {
            var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
            var state = new AuthenticationState(anonymous);
            return state;
        }

        public async void SetLogoutState()
        {
            await localStorageService.RemoveAsync("imp-inf");

            NotifyAuthenticationStateChanged(Task.FromResult(GetStateAnonymous()));
        }
    }
}
