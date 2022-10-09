﻿using TelegramBot.Data.Entities;
using TgBotLib.Services;

namespace TelegramBot.Services.ApiServices
{
    public class LessonsService : BaseCRUDService<Lesson, int>
    {
        public LessonsService() : base(AppSettings.LessonsRoot)
        {
        }
    }
}