# Books MVC

Тестовое задание — приложение для управления домашней библиотекой.

Приложение реализовано на ASP.NET Core MVC с использованием SQL Server и
хранимых процедур.

## Технологии

- .NET 10
- ASP.NET Core MVC
- C#
- SQL Server 2022
- Docker / Docker Compose
- ADO.NET
- Bootstrap
- XML

## Возможности

- просмотр списка книг;
- просмотр карточки книги;
- создание книги;
- редактирование книги;
- удаление книги;
- хранение оглавления книги в XML;
- отображение оглавления в виде списка глав;
- выборка данных из XML;
- работа с базой данных через хранимые процедуры.

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