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

        public void SetInlineButton(string markup) => SetInlineButtons(new List<string> { markup });
        public void SetInlineButtons(List<string> markup) => SetInlineButtons(new List<List<string>> { markup });
        public void SetInlineButtons(List<List<string>> markup) => AddButtons(markup, (lineMarkup) => lineMarkup.Select(text => InlineKeyboardButton.WithCallbackData(text, "@" + text)).ToList());

        public void SetInlineButton((string, string) markup) => SetInlineButtons(new List<(string, string)> { markup });
        public void SetInlineButtons(List<(string name, string callback)> markup) => SetInlineButtons(new List<List<(string name, string callback)>> { markup });
        public void SetInlineButtons(List<List<(string name, string callback)>> markup) => AddButtons(markup, (lineMarkup) => lineMarkup.Select(b => InlineKeyboardButton.WithCallbackData(b.name, "@" + b.callback)).ToList());

        public void SetInlineUrlButtons(List<List<(string name, string url)>> markup) => AddButtons(markup, (lineMarkup) => lineMarkup.Select(b => InlineKeyboardButton.WithUrl(b.name, b.url)).ToList());
        public void SetInlineUrlButtons(List<(string name, string url)> markup) => SetInlineUrlButtons(new List<List<(string, string)>> { markup });

        private void AddButtons<T>(List<List<T>> markup, Func<List<T>, List<InlineKeyboardButton>> createLine)
        {
            foreach (var lineMarkup in markup)
            {
                returnsButtons.Add(createLine(lineMarkup));
            }
        }
    }
}
