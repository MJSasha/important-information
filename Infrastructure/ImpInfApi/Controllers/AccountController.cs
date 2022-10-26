using ImpInfApi.Repository;
using ImpInfCommon.Data.Models;
using ImpInfCommon.Data.Other;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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
        public async Task<ObjectResult> Registrate([FromBody] RegistrationModel registrationModel, long chatId)
        {
            User user = new()
            {
                Name = registrationModel.Name,
                Phone = registrationModel.Phone,
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
            User user = await usersRepository.ReadFirst(u => u.Login == authModel.Login && u.Password.Value == authModel.Password, u => u.Password);
            if (user != null)
            {
                var token = Guid.NewGuid().ToString();
                user.Token = token;
                await usersRepository.Update(user);
                return Ok(user);
            }
            return StatusCode(401, "User not found.");
        }

        [HttpGet("CheckToken/{token}")]
        public async Task<bool> CheckToken(string token)
        {
            User user = await usersRepository.ReadFirst(u => u.Token == token);
            return user != null;
        }
    }
}
