using System;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using TelegramBot.Messages;
using TelegramBot.Services;

namespace TelegramBot
{
    internal class Program
    {
        [Obsolete]
        static void Main(string[] args)
        {
            try
            {
                var client = SingletonService.GetClient();

                client.StartReceiving();

                LogService.LogStart();

                client.OnMessage += OnMessageHandler;
                client.OnMessage += LogService.LogMessages;
                client.OnCallbackQuery += OnCallbackQweryHandlerAsync;
                client.OnCallbackQuery += LogService.LogCallbacks;

                Console.ReadLine();
                client.StopReceiving();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
            }
        }

        [Obsolete]
        private static async void OnCallbackQweryHandlerAsync(object sender, CallbackQueryEventArgs e)
        {
            MessageCollector message = new(e.CallbackQuery.Message.Chat.Id);
            MessagesTexts messagestexs = new(e.CallbackQuery.Message.Chat.Id);

            Func<Task> response = e.CallbackQuery.Data switch
            {
                "@О нас" => messagestexs.Information(),
                _ => message.UnknownMessage()
            };

            await response();
        }

        [Obsolete]
        private static async void OnMessageHandler(object sender, MessageEventArgs e)
        {
            MessageCollector message = new(e.Message.Chat.Id);

            Func<Task> response = e.Message.Text switch
            {
                "/start" => message.StartMenu(),
                "Привет" => message.SendText("Привет"),
                _ => message.UnknownMessage()
            };

            await response();
        }
    }
}
