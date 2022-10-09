using ImpInfApi.Repository;
using ImpInfCommon.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ImpInfApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class UsersController : BaseCrudController<User>
    {
        private readonly BaseCrudRepository<User> repository;

        public UsersController(BaseCrudRepository<User> repository) : base(repository)
        {
            this.repository = repository;
        }

        [HttpGet("ByChatId/{chatId}")]
        public Task<User> GetByChatId(long chatId)
        {
            return repository.ReadFirst(u => u.ChatId == chatId);
        }
    }
}
