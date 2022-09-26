namespace TelegramBot.Data
{
    public static class Texts
    {
        public static string StartMenu { get; } = "Добро пожаловать в чат Важной информации.\nЧто бы вы хотели узнать?";
        public static string AboutUs { get; } = "Наша команда постаралась над тем, чтобы ты удобно и быстро получал актуальную информацию об учебном процессе. \n " +
            "👋Давай познакомимся:\n\n" +
            "•\tАлександр Ушанков - _BackEnd_-разработчик, идейный вдохновитель, руководитель\n" +
            "•\tДанила Самсонов - _FrontEnd_-разработчик\n" +
            "•\tАлександр Мусалов - разработка _Telegram_ бота\n" +
            "•\tВладимир Резников - разработка _Telegram_ бота\n" +
            "•\tАлександр Бережной - разработка _Telegram_ бота\n" +
            "•\tЕва Пирожкова - разработка _Telegram_ бота";
        public static string Oops { get; } = "Упс, что-то пошло не так...";
        public static string AdminPanel { get; } = "Добро пожаловать в панель администратора 💻";
        public static string DetailLessonInfo { get; } = "Для просмотра детальной информации по предмету, нажмите на кнопку ⬇";
        public static string NextWeek { get; } = "Новости, созданные в промежуток с {0} по {1}\nДля перехода к другой неделе нажмите на кнопку";
        public static string UnknownMessage { get; } = "Пока я не понимаю данное сообщение, но скоро научусь";
        public static string ErrorAction { get; } = "Вы не обладаете правами для смены роли пользователя";
        public static string ChangeRole { get; } = "Роль выбранного пользователя изменена";
        public static string NullUser { get; } = "Такого пользователя не существует";
    }
}
