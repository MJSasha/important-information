using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBot.Interfaces;

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
        public virtual async Task ProcessMessage(Message message)
        {
            if (сancellationToken == null) await Task.Run(() => RegistrateProcessing());
            if (!сancellationToken.IsCancellationRequested) currentTask.Start();
        }

        [Obsolete]
        protected abstract void RegistrateProcessing();

        protected void AddProcessing(string message, Action action, Action completeAction = null)
        {
            сancellationToken = new();
            currentTask = new Task(() =>
            {
                action?.Invoke();
                сancellationToken.Cancel();
            });
            bot.SendMessage(message);
            currentTask.Wait();

            completeAction?.Invoke();
        }
    }
}
