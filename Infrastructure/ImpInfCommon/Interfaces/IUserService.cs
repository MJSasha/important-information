using ImpInfCommon.Data.Models;
using Refit;
using System.Threading.Tasks;

namespace ImpInfCommon.Interfaces
{
    public interface IUserService : ICrudService<User, int>
    {
        [Get("/ByChatId/{chatId}")]
        Task<User> GetByChatId(long chatId);
    }
}
