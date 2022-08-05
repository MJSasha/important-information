# BackEnd

## Запуск

*BackEnd* запускается через *Docker* из консоли, которую необходимо открыть в этой папке.
Сама команда имеет вид:

> docker-compose up

Для остановки работы API и удаления контейнера нужно ввести команду

> docker-compose down

При этом запустится сборка *backend* со следующим содержимым:

|Название контейнера|Описание|Порт|
|-|-|-|
|*backend*|API|8080| 
|*imp-inf-db*|База данный MySQL|3306|
|*adminer*|Админка для просмотра данных в бд|5000|

**Тестовые модели добавляются автоматически, их можно посмотреть в файле [data.sql](https://github.com/MJSasha/important-information/blob/main/BackEnd/src/main/resources/data.sql)**

## Документация

Документация сделана при помощи *Postman* и доступна по
[ссылке](https://documenter.getpostman.com/view/19981559/Uz5GpGt3).

*DockerHub* проекта доступен по [ссылке](https://hub.docker.com/repository/docker/mjsasha/backend_important-information).
