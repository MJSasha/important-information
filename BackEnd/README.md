# BackEnd

## Запуск

*BackEnd* запускается через *Docker* из консоли, которую необходимо открыть в этой папке.
Сама команда имеет вид:

> docker-compose up

Для остановки работы API и удаления контейнера нужно ввести команду

> docker-compose down

При этом запустится сборка *backend* со следующим содержимым:

- *backend* - API

**Тестовые модели добавляются автоматически, их можно посмотреть в файле [data.sql](/src/main/resources/data.sql)**

*API* доступна по локальному порту 8080


## Документация

Документация сделана при помощи *Postman* и доступна по
[ссылке](https://documenter.getpostman.com/view/19981559/Uz5GpGt3).

*DockerHub* проекта доступен по [ссылке](https://hub.docker.com/repository/docker/mjsasha/backend_important-information).
