using ImpInfCommon.Interfaces;
using System;
using System.Threading.Tasks;

namespace TelegramBot.Services
{
    public class ErrorsHandler : IErrorsHandler
    {
        public void ProcessError(Exception ex)
        {
            throw ex;
        }

        public Task SaveExecute(Func<Task> action)
        {
            return action();
        }
    }
}
