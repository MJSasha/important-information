using ImpInfCommon.Data.Models;
using System.Threading.Tasks;

namespace ImpInfCommon.Interfaces
{
    public interface INotificationsService
    {
        Task NotifyNewsCreated(News news);
    }
}
