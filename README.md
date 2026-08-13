# Books MVC

Тестовое задание — приложение для управления домашней библиотекой.

Приложение реализовано на ASP.NET Core MVC с использованием SQL Server и
хранимых процедур.

## Запуск
### Вариант 1 — Docker
Для запуска через Docker:

```bash

docker compose up

```text

### Вариант 2 — SQL Server Express
Если Docker не установлен:

Установить SQL Server Express.
Запустить SQL Server.
Выполнить Database/init.sql в SQL Server Management Studio.
Проверить connection string в appsettings.json и при необходимости
изменить имя SQL Server instance.
Запустить приложение.

## Структура проекта
Books.Mvc/
├── Controllers/
│   └── BooksController.cs
│
├── Interfaces/
│   └── IBookRepository.cs
│
├── Models/
│   └── Book.cs
│
├── Repositories/
│   └── BookRepository.cs
│
├── Services/
│   └── ...
│
├── ViewModels/
│   └── ...
│
├── Views/
│   ├── Books/
│   │   ├── Index.cshtml
│   │   ├── Details.cshtml
│   │   ├── Create.cshtml
│   │   └── Edit.cshtml
│   │
│   └── Shared/
│       └── _Layout.cshtml
│
├── Database/
│   └── init.sql
│
├── docker-compose.yml
└── README.md