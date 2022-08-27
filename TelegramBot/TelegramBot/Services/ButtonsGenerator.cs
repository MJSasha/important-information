using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Services
{
    public class ButtonsGenerator
    {
        private readonly List<List<InlineKeyboardButton>> returnsButtons = new();

        public IReplyMarkup GetButtons()
        {
            return new InlineKeyboardMarkup(returnsButtons);
        }

        public void SetInlineButtons(params string[] markup) => SetInlineButtons(new List<string>[] { markup.ToList() });
        public void SetInlineButtons(params List<string>[] markup) => AddButtons(markup.ToList(), (lineMarkup) => lineMarkup.Select(text => InlineKeyboardButton.WithCallbackData(text, "@" + text)).ToList());

        public void SetInlineButtons(params (string name, string callback)[] markup) => SetInlineButtons(new List<(string name, string callback)>[] { markup.ToList() });
        public void SetInlineButtons(params List<(string name, string callback)>[] markup) => AddButtons(markup.ToList(), (lineMarkup) => lineMarkup.Select(b => InlineKeyboardButton.WithCallbackData(b.name, "@" + b.callback)).ToList());

        public void SetInlineUrlButtons(params (string name, string url)[] markup) => SetInlineUrlButtons(new List<(string, string)>[] { markup.ToList() });
        public void SetInlineUrlButtons(params List<(string name, string url)>[] markup) => AddButtons(markup.ToList(), (lineMarkup) => lineMarkup.Select(b => InlineKeyboardButton.WithUrl(b.name, b.url)).ToList());

        private void AddButtons<T>(List<List<T>> markup, Func<List<T>, List<InlineKeyboardButton>> createLine)
        {
            foreach (var lineMarkup in markup)
            {
                returnsButtons.Add(createLine(lineMarkup));
            }
        }
    }
}
