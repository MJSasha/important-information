using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.Interfaces;
using Telegram.Bot.Types;
using Telegram.Bot.Args;

namespace TelegramBot.Handlers
{
    public abstract class BaseSpecialHandler
    {
        protected Task currentTask;
        protected CancellationTokenSource сancellationToken;
        protected readonly IBotService bot;

        protected BaseSpecialHandler(IBotService bot)
        {
            this.bot = bot;
        }

        [Obsolete]
        public abstract Task ProcessMessage(Message registrationMassage);

        protected void AddProcessing(string message, Action action, Action completeAction = null)
        {
            сancellationToken = new();
            currentTask = new Task(() =>
            {
                action();
                сancellationToken.Cancel();
            });
            bot.SendMessage(message);
            currentTask.Wait();

            completeAction?.Invoke();
        }
    }
}
