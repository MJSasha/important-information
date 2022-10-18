using System.Threading.Tasks;

namespace ImpInfCommon.Interfaces
{
    public interface ITokenProvider
    {
        string GetToken();
        Task SetToken();
    }
}
