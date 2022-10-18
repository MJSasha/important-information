using ImpInfCommon.Interfaces;
using ImpInfFrontCommon.Services;

namespace ImpInfWeb
{
    public class TokenProvider : ITokenProvider
    {
        private readonly CookieService cookieService;
        private string Token { get; set; } = "";

        public TokenProvider(CookieService cookieService)
        {
            this.cookieService = cookieService;
        }

        public string GetToken()
        {
            return Token;
        }

        public async Task SetToken()
        {
            Token = await cookieService.GetCookies("token");
        }
    }
}