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
    public class UsersController : BaseCrudController<User, int>
    {
        private readonly BaseCrudRepository<User> repository;

        public UsersController(BaseCrudRepository<User> repository) : base(repository, (User user, int id) => user.Id == id)
        {
            this.repository = repository;
        }

        [HttpPatch("{id}")]
        public override Task<ObjectResult> Patch(int id, [FromBody] User user)
        {
            user.Id = id;
            return base.Patch(id, user);
        }

        [HttpGet("ByChatId/{chatId}")]
        public Task<User> GetByChatId(long chatId)
        {
            return repository.ReadFirst(u => u.ChatId == chatId);
        }
    }
}
