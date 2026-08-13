using Books.Mvc.ViewModels;

namespace Books.Mvc.Interfaces;

public interface IBookService
{
    Task<BookDetailsViewModel?> GetDetailsAsync(int id);
}