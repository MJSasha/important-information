using System;
using System.Threading.Tasks;

namespace ImpInfCommon.Interfaces
{
    public interface IErrorsHandler
    {
        public void ProcessError(Exception ex);
        public Task SaveExecute(Func<Task> action);
    }
}
