using ImpInfCommon.Data.Models;
using System.Net.Http;
using System;
using System.Threading.Tasks;

namespace ImpInfCommon.Interfaces
{
    public interface IUserService : IBaseCRUDService<User, int>
    {
        Task<User> GetByChatId(long chatId);
    }
}
