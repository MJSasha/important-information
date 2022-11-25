using ImpInfApi.Hubs;
using ImpInfCommon.Data.Models;
using ImpInfCommon.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ImpInfApi.Services
{
    public class NotificationsService : INotificationsService
    {
        private readonly IHubContext<NotificationsHub> hubContext;

        public NotificationsService(IHubContext<NotificationsHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public Task NotifyNewsCreated(News news) => hubContext.Clients.All.SendAsync(nameof(INotificationsService.NotifyNewsCreated), news);
    }
}
