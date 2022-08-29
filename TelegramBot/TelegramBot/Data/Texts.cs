namespace TelegramBot.Data
{
    public static class Texts
    {
        public static string StartMenu { get; } = "Доброе пожаловать в чат Важной информации.\nЧто бы вы хотели узнать?";
        public static string AboutUs { get; } = "Наша команда постаралась над тем, чтобы ты удобно и быстро получал актуальную информацию об учебном процессе. \n " +
            "👋Давай познакомимся:\n" +
            "•\tАлександр Ушанков - BackEnd-разработчик,идейный\n" +
            "\tвдохновитель, руководитель.\n" +
            "•\tДанила Самсонов - FrontEnd-разработчик.\n" +
            "•\tАлександр Мусалов - разработка Telegram бота.\n" +
            "•\tВладимир Резников - разработка Telegram бота.\n" +
            "•\tАлександр Бережной - разработка Telegram бота.\n" +
            "•\tЕва Пирожкова - разработка Telegram бота.";
        public static string Oops { get; } = "Упс, что-то пошло не так...";
        public static string AdminPanel { get; } = "Добро пожаловать в панель администратора💻";
        public static string DetailLessonInfo { get; } = "Для просмотра детальной информации по предмету, нажмите на кнопку⬇";
        public static string NextWeek { get; } = "Для перехода к другой неделе нажмите на кнопку";
        public static string UnknownMessage { get; } = "Пока я не понимаю данное сообщение, но скоро научусь";
    }
}
