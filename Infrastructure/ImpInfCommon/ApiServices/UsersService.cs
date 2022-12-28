using ImpInfCommon.Data.Models;
using ImpInfCommon.Interfaces;
using ImpInfCommon.Utils;
using System.Net.Http;
using System.Threading.Tasks;

namespace ImpInfCommon.ApiServices
{
    public class UsersService : BaseCRUDService<User, int>, IUserService
    {
        private readonly IUserService userService;
        private readonly IErrorsHandler errorsHandler;

        public UsersService(HttpClient httpClient, IErrorsHandler errorsHandler) : base(httpClient, errorsHandler)
        {
            userService = UtilsFunctions.GetRefitService<IUserService>(httpClient); ;
            this.errorsHandler = errorsHandler;
        }

        public async Task<User> GetByChatId(long chatId)
        {
            User result = default;
            await errorsHandler.SaveExecute(async () => result = await userService.GetByChatId(chatId));
            return result;
        }
    }
}
