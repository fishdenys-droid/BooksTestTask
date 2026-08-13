namespace Books.Mvc.ViewModels;

public class BookDetailsViewModel
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public int? YearPublished { get; set; }

    public List<ChapterViewModel> Chapters { get; set; } = [];
}