# Books MVC

Тестовое задание — приложение для управления домашней библиотекой.

Приложение реализовано на ASP.NET Core MVC с использованием SQL Server и
хранимых процедур.

## Запуск

Для запуска через Docker:

```bash
docker compose up


## Структура проекта

```text
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