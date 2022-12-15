using ImpInfCommon.Data.Models;
using System.Threading.Tasks;

namespace ImpInfCommon.Interfaces
{
    public interface IUserService : ICrudService<User, int>
    {
        Task<User> GetByChatId(long chatId);
    }
}
