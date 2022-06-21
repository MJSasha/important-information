using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using TelegramBot.Messages;
using TelegramBot.Models;
using TelegramBot.Repositories;


namespace TelegramBot
{
    internal class Program
    {
        [Obsolete]
        static async Task Main(string[] args)
        {
            if (true)
            {
                // Post()
                PostDay newDay = new PostDay();

                PostDayRepository newDayRepository = new PostDayRepository();
                var response = await newDayRepository.Post(newDay);
                Console.WriteLine("POST: response = " + response.StatusCode);

                // GET()
                var daysRepo = new DayRepository();
                var days = await daysRepo.Get();
                SendDays(days);
                

                return;
            }
            ///      
            try
            {
                var client = new TelegramBotClient(AppSettings.Token);
                client.StartReceiving();
                client.OnMessage += OnMessageHandler;
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
        public static void SendDays(List<Day> days)
        {

            foreach (var item in days)
            {
                Console.WriteLine("Id = " + item.Id);
                Console.WriteLine("Date = " + item.Date);
                Console.WriteLine("Information = " + item.Information);
                try
                {
                    Console.WriteLine("LessonsAndTimes = " + item.LessonsAndTimes[0].Id);
                    Console.WriteLine("LessonsAndTimes = " + item.LessonsAndTimes[0].Lesson);
                    Console.WriteLine("LessonsAndTimes = " + item.LessonsAndTimes[0].Time);
                }
                catch (Exception)
                {
                    Console.WriteLine(" LessonsAndTimes.Count = " + item.LessonsAndTimes.Count.ToString());
                }  
            }

        }

        public static void SendUsers(List<User> users)
        {
            foreach (var item in users)
            {
                Console.WriteLine("Id = " + item.Id);
                Console.WriteLine("Login = " + item.Login);
                Console.WriteLine("Token = " + item.Token);
                Console.WriteLine("Name = " + item.Name);
                Console.WriteLine("Role = " + item.Role);
                Console.WriteLine();
            }
        }

        public static void SendUser(User user)
        {
            Console.WriteLine("Id = " + user.Id);
            Console.WriteLine("Login = " + user.Login);
            Console.WriteLine("Name = " + user.Name);
            Console.WriteLine("Role = " + user.Role);
            Console.WriteLine();
        }

    }
}
