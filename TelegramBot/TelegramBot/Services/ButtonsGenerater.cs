using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Services
{
    public static class ButtonsGenerater
    {
        /// <summary>
        /// Возвращает кнопки с колбеком эквивалентным названию со знаком "@"
        /// </summary>
        /// <param name="markup">Разметка кнопок. первый лист - строчки, второй - столбцы</param>
        /// <returns></returns>
        public static IReplyMarkup GetInlineByttons(List<List<string>> markup)
        {
            List<List<InlineKeyboardButton>> returnsButtons = new();

            foreach (var lines in markup)
            {
                List<InlineKeyboardButton> buttonsLine = new();
                lines.ForEach(text => buttonsLine.Add(InlineKeyboardButton.WithCallbackData(text, "@" + text)));
                returnsButtons.Add(buttonsLine);
            }

            return new InlineKeyboardMarkup(returnsButtons);
        }

        /// <summary>
        /// Возвращает кнопки с колбеком (@ ставится автоматически)
        /// </summary>
        /// <param name="markup">Разметка кнопок. Первый лист - строчки, второй - столбцы</param>
        /// <returns></returns>
        public static IReplyMarkup GetInlineByttonsWithCustomCollback(List<List<(string name, string callback)>> markup)
        {
            List<List<InlineKeyboardButton>> returnsButtons = new();

            foreach (var lines in markup)
            {
                List<InlineKeyboardButton> buttonsLine = new();
                lines.ForEach(text => buttonsLine.Add(InlineKeyboardButton.WithCallbackData(text.name, "@" + text.callback)));
                returnsButtons.Add(buttonsLine);
            }

            return new InlineKeyboardMarkup(returnsButtons);
        }
    }
}
