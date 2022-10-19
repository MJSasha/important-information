using ImpInfCommon.ApiServices;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace ImpInfFrontCommon.Services
{
    public class TokenAuthStateProvider : AuthenticationStateProvider
    {
        private readonly CookieService cookieService;
        private readonly AuthService authService;

        public TokenAuthStateProvider(CookieService cookieService, AuthService authService)
        {
            this.cookieService = cookieService;
            this.authService = authService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await cookieService.GetCookies("token");
            if (string.IsNullOrWhiteSpace(token) || !await authService.CheckToken(token)) return GetStateAnonymous();

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Authentication, token),
                new Claim(ClaimTypes.Role, "User")
            };

            var claimsIdentity = new ClaimsIdentity(claims, "Token");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            return new AuthenticationState(claimsPrincipal);
        }

        public async void SetLogoutState()
        {
            await cookieService.SetCookies("token", "");

            NotifyAuthenticationStateChanged(Task.FromResult(GetStateAnonymous()));
        }

        private static AuthenticationState GetStateAnonymous()
        {
            var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
            var state = new AuthenticationState(anonymous);
            return state;
        }
    }
}
