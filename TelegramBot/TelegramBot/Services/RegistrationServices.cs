using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TelegramBot.Data.ViewModels;
using TelegramBot.Interfaces;
using TelegramBot.Messages;
using TelegramBot.Services;

namespace TelegramBot.Services
{
    public class RegistrationServices
    {
        private static Task One;
        private static RegistrationModel Model = new RegistrationModel();
        private static CancellationTokenSource ts;
        private IBotService bot;
        private static long ChatId;
        private static List<long> BusyUserId = DistributionService.BusyUserId; 
        private static string data;

        public static Func<Task> AddToRegistration(long chatId)
        {
            BusyUserId.Add(chatId);
            ChatId = chatId;
            Task addToRegistration = new(() => BusyUserId.Add(chatId));
            return () => addToRegistration;
        }

        public static Func<Task> EndToRegistration(long chatId)
        {
            var busyUserId = DistributionService.BusyUserId;
            BusyUserId.Remove(chatId);

            ChatId = chatId;
            Task addToRegistration = new(() => busyUserId.Remove(chatId));
            return () => addToRegistration;
        }

        [Obsolete]
        public async Task StartRegistration(long chatId, string text)
        {
            data = text;    
            if (ts==null) await Task.Run(() => IsSuccessRegistration());
            if (!ts.IsCancellationRequested) One.Start();
        }

        [Obsolete]
        private void IsSuccessRegistration()
        {
            bot = new BotService(ChatId);

            try
            {
                ts = new CancellationTokenSource();
                One = new Task(RegName);
                bot.SendMessage("enter name:");
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
                bot.SendMessage("enter email:");

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
                bot.SendMessage("enter password:");

                One = new Task(RegPass);
                One.Wait(ts.Token);
            }
            catch (SystemException)
            {
                ts.Dispose();
            }

             bot.SendMessage("successfull");
             bot.SendMessage(Model.Name);
             bot.SendMessage(Model.Email);
             bot.SendMessage(Model.Password);
             EndToRegistration(ChatId);

        }
        private static void RegName()
        {
            if (RegistrationName(data))
            {
                ts.Cancel();
                Console.WriteLine("RegName is succesfull");
            }
            else throw new("RegName Bad");
        }
        private static void RegEmail()
        {
            if (RegistrationEmail(data))
            {
                ts.Cancel();
                Console.WriteLine("RegEmail is succesfull");
            }
            else throw new("RegEmail Bad");
        }
        private static void RegPass()
        {
            if (RegistrationPassword(data))
            {
                ts.Cancel();
                Console.WriteLine("RegPass is succesfull");
            }
            else throw new("RegistrationPassword Bad");
        }

        public static Boolean RegistrationName(string text)
        {
            Model.Name = text;
            return true;
        }

        public static Boolean RegistrationEmail(string text)
        {
            Model.Email = text;
            return true;
        }
        public static Boolean RegistrationPassword(string text)
        {
            Model.Password = text;
            return true;
        }

    }
}
