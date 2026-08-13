IF DB_ID('Books') IS NULL
BEGIN
    CREATE DATABASE Books;
END
GO

USE Books;
GO

-- =============================================
-- Таблица книг
-- =============================================

IF OBJECT_ID('dbo.Books', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Books
    (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Title NVARCHAR(200) NOT NULL,
        Author NVARCHAR(200) NOT NULL,
        YearPublished INT NULL,
        Contents XML NULL
    );
END
GO


-- =============================================
-- INSERT
-- =============================================

CREATE OR ALTER PROCEDURE dbo.Book_Insert
    @Title NVARCHAR(200),
    @Author NVARCHAR(200),
    @YearPublished INT = NULL,
    @Contents XML = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.Books
    (
        Title,
        Author,
        YearPublished,
        Contents
    )
    VALUES
    (
        @Title,
        @Author,
        @YearPublished,
        @Contents
    );

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS Id;
END
GO


-- =============================================
-- SELECT ALL
-- =============================================

CREATE OR ALTER PROCEDURE dbo.Book_GetAll
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        Title,
        Author,
        YearPublished,
        Contents
    FROM dbo.Books
    ORDER BY Id;
END
GO


-- =============================================
-- SELECT BY ID
-- =============================================

CREATE OR ALTER PROCEDURE dbo.Book_GetById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        Title,
        Author,
        YearPublished,
        Contents
    FROM dbo.Books
    WHERE Id = @Id;
END
GO


-- =============================================
-- UPDATE
-- =============================================

CREATE OR ALTER PROCEDURE dbo.Book_Update
    @Id INT,
    @Title NVARCHAR(200),
    @Author NVARCHAR(200),
    @YearPublished INT = NULL,
    @Contents XML = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.Books
    SET
        Title = @Title,
        Author = @Author,
        YearPublished = @YearPublished,
        Contents = @Contents
    WHERE Id = @Id;
END
GO


-- =============================================
-- DELETE
-- =============================================

CREATE OR ALTER PROCEDURE dbo.Book_Delete
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM dbo.Books
    WHERE Id = @Id;
END
GO


-- =============================================
-- Test data
-- =============================================

IF NOT EXISTS (SELECT 1 FROM dbo.Books)
BEGIN
    INSERT INTO dbo.Books
    (
        Title,
        Author,
        YearPublished,
        Contents
    )
    VALUES
    (
        N'Война и мир',
        N'Лев Толстой',
        1869,
        N'
        <contents>
            <chapter number="1">
                <title>Часть первая</title>
            </chapter>
            <chapter number="2">
                <title>Часть вторая</title>
            </chapter>
            <chapter number="3">
                <title>Часть третья</title>
            </chapter>
        </contents>'
    ),
    (
        N'Преступление и наказание',
        N'Фёдор Достоевский',
        1866,
        N'
        <contents>
            <chapter number="1">
                <title>Часть первая</title>
            </chapter>
            <chapter number="2">
                <title>Часть вторая</title>
            </chapter>
        </contents>'
    ),
    (
        N'Мастер и Маргарита',
        N'Михаил Булгаков',
        1967,
        N'
        <contents>
            <chapter number="1">
                <title>Никогда не разговаривайте с неизвестными</title>
            </chapter>
            <chapter number="2">
                <title>Понтий Пилат</title>
            </chapter>
            <chapter number="3">
                <title>Седьмое доказательство</title>
            </chapter>
        </contents>'
    );
END
GO