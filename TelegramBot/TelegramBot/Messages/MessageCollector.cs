using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using TelegramBot.Interfaces;
using TelegramBot.Services;
using TelegramBot.Messages;
using System.Threading;

namespace TelegramBot.Messages
{
    public class MessageCollector
    {
        private readonly IBotService bot;

        [Obsolete]
        public MessageCollector(long chatId)
        {
            bot = new BotService(chatId);
        }

        public Func<Task> StartMenu()
        {
            List<List<(string, string)>> markup = new()
            {
                new List<(string, string)>{ ("FirstButton",  "FirstButton")},
                //new List<(string, string)>{ "L2B1", "L2B2" },
                //new List<(string, string)>{ "L3B1" }
            };

            return () => bot.SendMessage("Стартовое меню", ButtonsGenerater.GetInlineButtons(markup));
        }


        private static Registration registrationUser = new Registration();
        private static CancellationTokenSource ts = new CancellationTokenSource();

        [Obsolete]
        public async Task RegistrationUsers(long chatId)
        {
            try
            {
                var client = new TelegramBotClient(AppSettings.Token);
                client.StartReceiving();
                client.OnMessage += OnMessageHandlerTwo;
                await Task.Run(() => IsSuccessRegistration());

            }
            catch
            {
                Console.WriteLine("ERROR");
            }
        }
        private static string data;

        private static Task One;
        private async void IsSuccessRegistration()
        {
            try
            {
                One = new Task(RegName);
                await bot.SendMessage("enter name:");
                One.Wait(ts.Token);
            }
            catch (SystemException)
            {
                ts.Dispose();
            }
            try
            {
                ts = new CancellationTokenSource();
                One = new Task(RegEmail);
                await bot.SendMessage("enter email:");

                One = new Task(RegEmail);
                One.Wait(ts.Token);
            }
            catch (SystemException)
            {
                ts.Dispose();
            }
            try
            {
                ts = new CancellationTokenSource();
                await bot.SendMessage("enter password:");

                One = new Task(RegPass);
                One.Wait(ts.Token);
            }
            catch (SystemException)
            {
                ts.Dispose();
            }

            await bot.SendMessage("successfull");
            await bot.SendMessage(registrationUser.Name);
            await bot.SendMessage(registrationUser.Email);
            await bot.SendMessage(registrationUser.Password);

        }


        [Obsolete]
        private static void OnMessageHandlerTwo(object sender, MessageEventArgs e)
        {
            if (e.Message.Text != "/reg")
            {
                Console.WriteLine("OnMessageHandlerTwo: " + e.Message.Text);
                data = e.Message.Text;
                if (!ts.IsCancellationRequested) One.Start();
            }
        }

        private static void RegName()
        {
            if (registrationUser.RegistrationName(data))
            {
                ts.Cancel();
                Console.WriteLine("RegName is succesfull");
            }
            else throw new("RegName Bad");
        }
        private static void RegEmail()
        {
            if (registrationUser.RegistrationEmail(data))
            {
                ts.Cancel();
                Console.WriteLine("RegEmail is succesfull");
            }
            else throw new("RegEmail Bad");
        }
        private static void RegPass()
        {
            if (registrationUser.RegistrationPassword(data))
            {
                ts.Cancel();
                Console.WriteLine("RegPass is succesfull");
            }
            else throw new("RegistrationPassword Bad");
        }

        public Func<Task> SendText(string text)
        {
            return () => bot.SendMessage(text);
        }
        public Func<Task> UnknownMessage()
        {
            return () => bot.SendMessage("Пока я не понимаю данное сообщение, но скоро научусь");
        }
    }
}
