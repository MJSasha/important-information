using Microsoft.JSInterop;

namespace ImpInfFrontCommon.Services
{
    public class CookieService
    {
        private readonly IJSRuntime jsRuntime;

        public CookieService(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        public async Task SetCookies(string key, string value)
        {
            await jsRuntime.InvokeAsync<object>("WriteCookie.WriteCookie", key, value, DateTime.Now.AddMinutes(1));
        }

        public async Task<string> GetCookies(string key)
        {
            return await jsRuntime.InvokeAsync<string>("ReadCookie.ReadCookie", key);
        }
    }
}
