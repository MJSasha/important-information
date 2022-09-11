using ImpInfApi.Data.Definitions;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ImpInfApi.Utils
{
    public class BeforeRequestHandler
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings appSettings;

        public BeforeRequestHandler(RequestDelegate next, AppSettings appSettings)
        {
            _next = next;
            this.appSettings = appSettings;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Cookies["token"] == appSettings.StaticToken)
            {
                var identity = GetIdentity();

                var jwtToken = new JwtSecurityToken(issuer: appSettings.Jwt.Issuer,
                                               audience: appSettings.Jwt.Audience,
                                               notBefore: DateTime.UtcNow,
                                               claims: identity.Claims,
                                               expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(appSettings.Jwt.Lifetime)),
                                               signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.Jwt.Key)), SecurityAlgorithms.HmacSha256));

                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwtToken);
                context.Request.Headers["Authorization"] = $"Bearer {encodedJwt}";
            }
            await _next(context);
        }

        private static ClaimsIdentity GetIdentity()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultRoleClaimType, Role.ADMIN.ToString())
            };
            ClaimsIdentity claimsIdentity = new(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }
    }
}
