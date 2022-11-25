using ImpInfCommon.Data.Models;
using SignalRSwaggerGen.Attributes;
using System.Threading.Tasks;

namespace ImpInfCommon.Interfaces
{
    [SignalRHub]
    public interface INotificationsService
    {
        Task NotifyNewsCreated(News news);
    }
}
