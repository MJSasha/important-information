using ImpInfCommon.Data.Models;
using System.Threading.Tasks;

namespace ImpInfCommon.Interfaces
{
    public interface IUser : ICrud<User, int>
    {
        Task<User> GetByChatId(long chatId);
    }
}
