using Books.Mvc.Interfaces;
using Books.Mvc.ViewModels;
using System.Xml;
using System.Xml.Linq;

namespace Books.Mvc.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _repository;

    public BookService(IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task<BookDetailsViewModel?> GetDetailsAsync(int id)
    {
        var book = await _repository.GetByIdAsync(id);

        if (book == null)
            return null;

        var chapters = ParseChapters(book.Contents);

        return new BookDetailsViewModel
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            YearPublished = book.YearPublished,
            Chapters = chapters
        };
    }

    private static List<ChapterViewModel> ParseChapters(string? xml)
    {
        if (string.IsNullOrWhiteSpace(xml))
            return [];

        XDocument document;

        try
        {
            document = XDocument.Parse(xml);
        }
        catch (XmlException)
        {
            return [];
        }

        return document
            .Descendants("chapter")
            .Select(x => new ChapterViewModel
            {
                Number = (int?)x.Attribute("number") ?? 0,
                Title = (string?)x.Element("title") ?? string.Empty
            })
            .ToList();
    }
}