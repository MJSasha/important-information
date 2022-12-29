using ImpInfCommon.ApiServices;
using ImpInfCommon.Data.Models;
using ImpInfCommon.Interfaces;
using ImpInfFrontCommon.Utils;
using Microsoft.AspNetCore.SignalR.Client;

namespace ImpInfFrontCommon.Services
{
    public class NotificationsService
    {
        public static Func<News, Task> OnNewsCreated { get; set; }

        public static async Task Init(string hubRoot)
        {
            var hubConnection = new HubConnectionBuilder().WithUrl(hubRoot, options =>
            {
                options.HttpMessageHandlerFactory = innerHandler => new IncludeRequestCredentialsMessageHandler { InnerHandler = innerHandler };
            }).Build();

            hubConnection.On<News>(nameof(INotificationsService.NotifyNewsCreated), (news) => OnNewsCreated?.Invoke(news)!);

            await hubConnection.StartAsync();
        }
    }
}
