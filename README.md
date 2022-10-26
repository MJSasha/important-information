# **<p align="center">Important information</p>**

<img alt="GitHub watchers" src="https://img.shields.io/github/watchers/MJSasha/important-information?style=social">
<img alt="GitHub contributors" src="https://img.shields.io/github/contributors/MJSasha/important-information">
<img alt="GitHub last commit" src="https://img.shields.io/github/last-commit/MJSasha/important-information">

## О проекте

Данный проект является учебным. Идея проекта состоит в разработке веб приложения, в котором будет поддерживаться актуальное расписание пар, зачетов и экзаменов, вестись лента новостей с информацией по предметам. Также предполагается наличие системы с отображением домашнего задания и возможностью выгрузки своих решений, добавления ссылок на полезные источники и т.д.

### Стек проекта

![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)
![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![JavaScript](https://img.shields.io/badge/javascript-%23323330.svg?style=for-the-badge&logo=javascript&logoColor=%23F7DF1E)
![React](https://img.shields.io/badge/react-%2320232a.svg?style=for-the-badge&logo=react&logoColor=%2361DAFB)
![MySQL](https://img.shields.io/badge/mysql-%2300f.svg?style=for-the-badge&logo=mysql&logoColor=white)

## Структура проекта

|Название|Описание|Порт|
|-|-|-|
|[FrontEnd](https://github.com/MJSasha/important-information/tree/main/FrontEnd)|*React* приложение, работает с [ImpInfApi](https://github.com/MJSasha/important-information/tree/main/Infrastructure/ImpInfApi)|3000|
|[ImpInfApi](https://github.com/MJSasha/important-information/tree/main/Infrastructure/ImpInfApi)|*API* на *.NET*, работает с БД на *MySQL*|8080|
|[ImpInfApp](https://github.com/MJSasha/important-information/tree/main/Infrastructure/ImpInfApp)|Приложение на базе *MAUI Blazor*|
|[ImpInfCommon](https://github.com/MJSasha/important-information/tree/main/Infrastructure/ImpInfCommon)|Общие классы для всех составляющих проекта на *.NET*|
|[ImpInfFrontCommon](https://github.com/MJSasha/important-information/tree/main/Infrastructure/ImpInfFrontCommon)|Общие классы и разметка для веб приложения и *MAUI*|
|[ImpInfWeb](https://github.com/MJSasha/important-information/tree/main/Infrastructure/ImpInfWeb)|Веб приложение на *Blazor WASM*|7132, 5132|
|[TelegramBot](https://github.com/MJSasha/important-information/tree/main/Infrastructure/TelegramBot)|Телеграмм бот на базе C#|
|[tg-bot-lib](https://github.com/MJSasha/tg-bot-lib/tree/8f769557b5574850437ab60990df3bdbb88d72c5)|Библиотека с основными классами для телеграмм бота|

## Полезные команды

### FrontEnd

Загрузка всех зависимостей

``` bash
npm i
```

Запуск приложения

``` bash
npm i
```

### ImpInfApi

В папке с *ImpInfApi* лежит *docker-compose* файл, от которого можно запустить:

БД *MySQL*

``` bash
docker-compose up imp-inf-db
```

Сервис Adminer для администрирования БД (открывается на порте 5000)

``` bash
docker-compose up adminer
```
