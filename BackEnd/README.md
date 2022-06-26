# BackEnd

## Запуск

*BackEnd* запускается через *Docker* из консоли, которую необходимо открыть в этой папке.
Сама команда имеет вид:

> docker-compose up

Для остановки работы API и удаления контейнера нужно ввести команду

> docker-compose down

При этом запустится сборка *backend* со следующим содержимым:

- *backend* - API
- *imp-inf-db* - база данных
- *pgadmin* - визуальный интерфейс для работы с БД

**Тестовые модели добавляются автоматически, их можно посмотреть в файле [data.sql](/src/main/resources/data.sql)**

*API* доступна по локальному порту 8080

*PGAdmin* доступен по локальному порту 5050

## Запуск PGAdmin

1. Переходим на порт [5050](http://localhost:5050)
2. Вводил логин и пароль указанные в файле [*
   docker-compose.yml*](https://github.com/MJSasha/important-information/blob/main/BackEnd/docker-compose.yml)

<code>

    pgadmin:
        container_name: pgadmin
        image: dpage/pgadmin4
        restart: always
        ports:
            - "5050:80"
        environment:
        PGADMIN_DEFAULT_EMAIL:  ТУТ ЛОГИН
        PGADMIN_DEFAULT_PASSWORD: ТУТ ПАРОЛЬ

</code>

3. Зарегистрируйте сервер

<div align="center">

![](/BackEnd/ForReadMe/PGServer.png)

</div>

4. Вводим название сервера и логин пароль (как на картинке)

<div align="center">

![Reg1](/BackEnd/ForReadMe/PGReg1.png)
![Reg2](/BackEnd/ForReadMe/PGReg2.png)

</div>

## Документация

Документация сделана при помощи *Postman* и доступна по
[ссылке](https://documenter.getpostman.com/view/19981559/Uz5GpGt3).

*DockerHub* проекта доступен по [ссылке](https://hub.docker.com/repository/docker/mjsasha/backend_important-information).
