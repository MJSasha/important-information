using ImpInfCommon.Data.Models;
using System.Threading.Tasks;

namespace ImpInfCommon.Interfaces
{
    public interface IUser : IBaseCRUD<User>
    {
        Task<User> GetByChatId(long chatId);
    }
}
