using ImpInfApi.Hubs;
using ImpInfCommon.Data.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ImpInfApi.Services
{
    public class NotificationsService
    {
        private readonly IHubContext<NotificationsHub> hubContext;

        public NotificationsService(IHubContext<NotificationsHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public Task NotifyNewsCreated(News news) => hubContext.Clients.All.SendAsync("NewsCreated", news);
    }
}
