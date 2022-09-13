using ImpInfApi.Repository;
using ImpInfCommon.Data.Models;
using ImpInfCommon.Data.Other;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ImpInfApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AppSettings appSettings;
        private readonly BaseCrudRepository<User> usersRepository;
        private readonly ILogger<User> logger;

        public AccountController(AppSettings appSettings, BaseCrudRepository<User> usersRepository, ILogger<User> logger)
        {
            this.appSettings = appSettings;
            this.usersRepository = usersRepository;
            this.logger = logger;
        }

        [HttpPost("{chatId}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ObjectResult> Registrate([FromBody] RegistrationModel registrationModel, long chatId)
        {
            User user = new()
            {
                Name = registrationModel.Name,
                Login = registrationModel.Login,
                Password = new Password { Value = registrationModel.Password },
                ChatId = chatId
            };

            await usersRepository.Create(user);
            logger.LogInformation($"User registrate. Login: {user.Login}; Name: {user.Name}; ChatId: {user.ChatId}");
            return Ok("Registration successful.");
        }

        [HttpPost]
        public async Task<ObjectResult> Auth(AuthModel authModel)
        {
            try
            {
                var identity = await GetIdentity(authModel);

                var jwtToken = new JwtSecurityToken(issuer: appSettings.Jwt.Issuer,
                                               audience: appSettings.Jwt.Audience,
                                               notBefore: DateTime.UtcNow,
                                               claims: identity.Claims,
                                               expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(appSettings.Jwt.Lifetime)),
                                               signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.Jwt.Key)), SecurityAlgorithms.HmacSha256));

                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwtToken);
                logger.LogInformation($"User authorized. Login: {authModel.Login}");
                return Ok(encodedJwt);
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(401, ex.Message);
            }
        }

        private async Task<ClaimsIdentity> GetIdentity(AuthModel authModel)
        {
            User user = await usersRepository.ReadFirst(u => u.Login == authModel.Login && u.Password.Value == authModel.Password, u => u.Password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
                };
                ClaimsIdentity claimsIdentity = new(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            throw new UnauthorizedAccessException("Incorrect login or password.");
        }
    }
}
