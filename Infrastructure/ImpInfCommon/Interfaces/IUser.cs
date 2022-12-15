using ImpInfCommon.Data.Models;
using Refit;
using System.Threading.Tasks;

namespace ImpInfCommon.Interfaces
{
    public interface IUser : ICrud<User, int>
    {
        [Get("ByChatId/{chatId}")]
        Task<User> GetByChatId(long chatId);
    }
}
