using ImpInfApi.Controllers;
using ImpInfApi.Models;
using ImpInfApi.Repository;
using ImpInfCommon.Data.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImpInfApi.Utils
{
    public class CheckPermisionMidleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings appSettings;
        private readonly BaseCrudRepository<User> userRepository;
        private readonly List<AvailablePath> availablePaths;

        public CheckPermisionMidleware(RequestDelegate next, AppSettings appSettings, BaseCrudRepository<User> userRepository, List<AvailablePath> availablePaths)
        {
            _next = next;
            this.appSettings = appSettings;
            this.userRepository = userRepository;
            this.availablePaths = availablePaths;
        }

        public async Task Invoke(HttpContext context)
        {
            if (CheckPathAllow(context.Request.Path.Value, context.Request.Method)) await _next(context);
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

        private bool CheckPathAllow(string path, string method)
        {
            foreach (var availablePath in availablePaths)
            {
                if (availablePath.Path.Contains("..."))
                {
                    if (path.Contains(availablePath.Path[..^3]) && method == availablePath.Method.ToString().ToUpper()) return true;
                }
                else if (path.Contains(availablePath.Path) && method == availablePath.Method.ToString().ToUpper()) return true;
            }

            return false;
        }
    }
}
