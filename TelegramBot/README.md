# **<p align="center">Telegram Bot</p>**

## Запуск

Проект можно запустить командой

```bash
docker-compose up
```

Проект содержит подмодули, для загрузке подмодулей воспользуйтесь командой

```bash
git submodule init
git submodule update
```

### Переменные среды

|Название|Дефолтное значение|Описание|
|-|-|-|
|API_TOKEN|2065215367:AAHxs51AowRJAqefe3tvV7d5jn5nsC_-xDc|Токен доступа к ТГ боту|
|BOT_TOKEN|Fp9u5dsvcdM3XIm|Токен доступа к *BackEnd*|
|BACK_ROOT|<http://localhost:8080/api/>|Путь к серверу|
|FRONT_ROOT|google.com|Ссылка на сайт, который будет отображаться в "О Нас"|
