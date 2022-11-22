using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ImpInfApi.Hubs
{
    public class NotificationsHub : Hub
    {
        public async Task NotifyAsync(string message)
        {
            await Clients.All.SendAsync("Notify", message);
        }
    }
}
