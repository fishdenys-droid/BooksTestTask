using Books.Mvc.Interfaces;
using Books.Mvc.Models;
using Microsoft.Data.SqlClient;
using System.Xml.Linq;

namespace Books.Mvc.Repositories;

public class BookRepository: IBookRepository
{
    private readonly string _connectionString;

    public BookRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "Connection string 'DefaultConnection' not found.");
    }

    public async Task<List<Book>> GetAllAsync()
    {
        var books = new List<Book>();

        await using var connection = new SqlConnection(_connectionString);

        await using var command = new SqlCommand("Book_GetAll", connection)
        {
            CommandType = System.Data.CommandType.StoredProcedure
        };

        await connection.OpenAsync();

        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            books.Add(new Book
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Title = reader.GetString(reader.GetOrdinal("Title")),
                Author = reader.GetString(reader.GetOrdinal("Author")),
                YearPublished = reader.IsDBNull(reader.GetOrdinal("YearPublished"))
                    ? null
                    : reader.GetInt32(reader.GetOrdinal("YearPublished")),
                Contents = reader.IsDBNull(reader.GetOrdinal("Contents"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("Contents"))
            });
        }

        return books;
    }

    public async Task<Book?> GetByIdAsync(int id)
    {
        await using var connection = new SqlConnection(_connectionString);

        await using var command = new SqlCommand("Book_GetById", connection)
        {
            CommandType = System.Data.CommandType.StoredProcedure
        };

        command.Parameters.AddWithValue("@Id", id);

        await connection.OpenAsync();

        await using var reader = await command.ExecuteReaderAsync();

        if (!await reader.ReadAsync())
            return null;

        return new Book
        {
            Id = reader.GetInt32(reader.GetOrdinal("Id")),
            Title = reader.GetString(reader.GetOrdinal("Title")),
            Author = reader.GetString(reader.GetOrdinal("Author")),
            YearPublished = reader.IsDBNull(reader.GetOrdinal("YearPublished"))
                ? null
                : reader.GetInt32(reader.GetOrdinal("YearPublished")),
            Contents = reader.IsDBNull(reader.GetOrdinal("Contents"))
                ? null
                : reader.GetString(reader.GetOrdinal("Contents"))
        };
    }

    public async Task<int> CreateAsync(Book book)
    {
        await using var connection = new SqlConnection(_connectionString);

        await using var command = new SqlCommand("Book_Insert", connection)
        {
            CommandType = System.Data.CommandType.StoredProcedure
        };

        command.Parameters.AddWithValue("@Title", book.Title);
        command.Parameters.AddWithValue("@Author", book.Author);
        command.Parameters.AddWithValue(
            "@YearPublished",
            (object?)book.YearPublished ?? DBNull.Value);
        command.Parameters.AddWithValue(
            "@Contents",
            (object?)book.Contents ?? DBNull.Value);

        await connection.OpenAsync();

        var result = await command.ExecuteScalarAsync();

        return Convert.ToInt32(result);
    }

    public async Task UpdateAsync(Book book)
    {
        await using var connection = new SqlConnection(_connectionString);

        await using var command = new SqlCommand("Book_Update", connection)
        {
            CommandType = System.Data.CommandType.StoredProcedure
        };

        command.Parameters.AddWithValue("@Id", book.Id);
        command.Parameters.AddWithValue("@Title", book.Title);
        command.Parameters.AddWithValue("@Author", book.Author);
        command.Parameters.AddWithValue(
            "@YearPublished",
            (object?)book.YearPublished ?? DBNull.Value);
        command.Parameters.AddWithValue(
            "@Contents",
            (object?)book.Contents ?? DBNull.Value);

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();
    }
    public async Task DeleteAsync(int id)
    {
        await using var connection = new SqlConnection(_connectionString);

        await using var command = new SqlCommand("Book_Delete", connection)
        {
            CommandType = System.Data.CommandType.StoredProcedure
        };

        command.Parameters.AddWithValue("@Id", id);

        await connection.OpenAsync();

        await command.ExecuteNonQueryAsync();
    }
}