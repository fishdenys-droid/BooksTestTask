using Books.Mvc.Models;

namespace Books.Mvc.Interfaces;

public interface IBookRepository
{
    Task<List<Book>> GetAllAsync();

    Task<Book?> GetByIdAsync(int id);

    Task<int> CreateAsync(Book book);

    Task UpdateAsync(Book book);

    Task DeleteAsync(int id);
}