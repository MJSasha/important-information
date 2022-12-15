using ImpInfApi.Repository;
using ImpInfCommon.Data.Models;
using ImpInfCommon.Data.Other;
using ImpInfCommon.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ImpInfApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase, IAuthService
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
        public async Task Registrate([FromBody] RegistrationModel registrationModel, long chatId)
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
        }

        [HttpPost]
        public async Task<User> Login(AuthModel authModel)
        {
            User user = await usersRepository.ReadFirst(u => u.Login == authModel.Login && u.Password.Value == authModel.Password, u => u.Password);
            if (user != null)
            {
                var token = Guid.NewGuid().ToString();
                user.Token = token;
                await usersRepository.Update(user);
            }
            return user;
        }

        [HttpGet("CheckToken/{token}")]
        public async Task<bool> CheckToken(string token)
        {
            User user = await usersRepository.ReadFirst(u => u.Token == token);
            return user != null;
        }

        [HttpGet("CurrentUser")]
        public Task<User> GetCurrentUser()
        {
            return usersRepository.ReadFirst(u => u.Token == HttpContext.Request.Cookies["token"]);
        }
    }
}
