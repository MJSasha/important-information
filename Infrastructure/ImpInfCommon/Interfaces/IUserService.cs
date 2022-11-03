using ImpInfCommon.Data.Models;
using System.Net.Http;
using System;
using System.Threading.Tasks;

namespace ImpInfCommon.Interfaces
{
    public interface IUser : ICrud<User>
    {
        Task<User> GetByChatId(long chatId);
    }
}
