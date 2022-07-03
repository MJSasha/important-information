using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using TelegramBot.Messages;

namespace TelegramBot
{
    internal class Program
    {
        private static List<long> RegistrationUsersChatId = new();
        public static TelegramBotClient client = new TelegramBotClient(AppSettings.Token);


        [Obsolete]
        static void Main(string[] args)
        {
            try
            {
                client.StartReceiving();
                client.OnMessage += OnMessageHandler;
                client.OnMessage += Test;
                client.OnCallbackQuery += Test;
                client.OnCallbackQuery += OnCallbackQweryHandlerAsync;
                Console.ReadLine();
                client.StopReceiving();
            }
            catch
            {
                Console.WriteLine("ERROR");
                Console.ReadLine();
            }
        }

        [Obsolete]
        private static async void OnCallbackQweryHandlerAsync(object sender, CallbackQueryEventArgs e)
        {
            MessageCollector message = new(e.CallbackQuery.Message.Chat.Id);

            Func<Task> response = e.CallbackQuery.Data switch
            {
                _ => message.UnknownMessage()
            };

            await response();
        }

        [Obsolete]
        public static async void OnMessageHandler(object sender, MessageEventArgs e)
        {
            MessageCollector message = new(e.Message.Chat.Id);
            Console.WriteLine(message);

            Func<Task> response = e.Message.Text switch
            {
                "/start" => message.StartMenu(),
                "/reg" => StartRegistration(e.Message.Chat.Id)

                ,
                "Привет" => message.SendText("Привет"),
                _ => message.UnknownMessage()
            };

            await response();
        }
        [Obsolete]
        public static Func<Task> StartRegistration(long chatId)
        {
            RegistrationUsersChatId.Add(chatId);
            client.StopReceiving();
            MessageCollector msg = new MessageCollector(chatId);
            return () => msg.RegistrationUsers(chatId);

        }


        [Obsolete]
        private static void Test(object sender, MessageEventArgs e)
        {
            Console.WriteLine(e.Message.Text);
        }

        [Obsolete]
        private static void Test(object sender, CallbackQueryEventArgs e)
        {
            Console.WriteLine(e.CallbackQuery.Data);

        }


    }
}
