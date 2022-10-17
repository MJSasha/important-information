using ImpInfApi.Repository;
using ImpInfCommon.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImpInfApi.Utils
{
    public class CheckPermisionMidleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings appSettings;
        private readonly BaseCrudRepository<User> userRepository;
        private Dictionary<string, string> allowsPaths;

        public CheckPermisionMidleware(RequestDelegate next, AppSettings appSettings, BaseCrudRepository<User> userRepository)
        {
            _next = next;
            this.appSettings = appSettings;
            this.userRepository = userRepository;

            allowsPaths = new();
            allowsPaths.Add("/api/Account", "POST");
        }

        public async Task Invoke(HttpContext context)
        {
            if (allowsPaths.ContainsKey(context.Request.Path) && allowsPaths.ContainsValue(context.Request.Method)) await _next(context);
            else
            {
                var token = context.Request.Cookies["token"];
                if (token != null && (await TokenIsValid(token) || appSettings.StaticToken == token)) await _next(context);
                else context.Response.StatusCode = 401;
            }
        }

        private async Task<bool> TokenIsValid(string token)
        {
            var user = await userRepository.ReadFirst(u => u.Token == token);
            return user != null;
        }
    }
}
