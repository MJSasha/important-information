using System.Net.Http;
using System.Threading.Tasks;

namespace ImpInfCommon.Interfaces
{
    public interface IBaseService
    {
        string Serialize<T>(T item);
        Task<T> Deserialize<T>(HttpResponseMessage httpResponse);
    }
}
