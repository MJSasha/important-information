using System;
using TelegramBot.Data.ViewModels;

namespace TelegramBot.Messages
{
    class Registration : RegistrationModel
    {
        public Registration() : base()
        {
        }

        // Проверка на допустимые значения
        public Boolean RegistrationName(string text)
        {
            Name = text;
            return true;
        }

        public Boolean RegistrationEmail(string text)
        {
            Email = text;
            return true;
        }
        public Boolean RegistrationPassword(string text)
        {
            Password = text;
            return true;
        }
    }
}
