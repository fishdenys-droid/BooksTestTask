using System.ComponentModel.DataAnnotations;

namespace Books.Mvc.Models
{

    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите название книги")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введите автора")]
        public string Author { get; set; } = string.Empty;

        public int? YearPublished { get; set; }

        public string? Contents { get; set; }
    }
}