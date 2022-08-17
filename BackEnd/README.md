# BackEnd

## Запуск

*BackEnd* запускается через *Docker* из консоли, которую необходимо открыть в этой папке.
Сама команда имеет вид:

```bash
docker-compose up
```

Для остановки работы API и удаления контейнера нужно ввести команду

```bash
docker-compose down
```

При этом запустится сборка *backend* со следующим содержимым:

|Название контейнера|Описание|Порт|
|-|-|-|
|*backend-api*|API|8080|
|*imp-inf-db*|База данный MySQL|3306|
|*adminer*|Админка для просмотра данных в бд|[5000](http://localhost:5000)|

**Тестовые модели добавляются автоматически, их можно посмотреть в файле [data.sql](https://github.com/MJSasha/important-information/blob/main/BackEnd/src/main/resources/data.sql)**

### Переменные среды

|Название|Дефолтное значение|Описание|
|-|-|-|
|MYSQL_HOST|0.0.0.0|Хост БД|
|MYSQL_PORT|3306|Порт БД|
|MYSQL_DB|imp-inf-db|Название БД|
|MYSQL_USER|admin|Пользователь БД|
|MYSQL_PASSWORD|admin|Пароль к БД|
|API_TOKEN|Fp9u5dsvcdM3XIm|Токен доступа внешних *API*|

## Документация

Документация сделана при помощи *Postman* и доступна по
[ссылке](https://documenter.getpostman.com/view/19981559/Uz5GpGt3).

*DockerHub* проекта:

- [*BackEnd*](https://hub.docker.com/repository/docker/mjsasha/backend_important-information)
- [*FrontEnd*](https://hub.docker.com/repository/docker/mjsasha/frontend_important-information)
