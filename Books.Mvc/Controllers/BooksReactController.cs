using Books.Mvc.Interfaces;
using Books.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace Books.Mvc.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksReactController : ControllerBase
    {
        private readonly IBookRepository _repository;

        public BooksReactController(IBookRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _repository.GetAllAsync();

            return Ok(books);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Book book)
        {
            if (!string.IsNullOrWhiteSpace(book.Contents))
            {
                try
                {
                    XDocument.Parse(book.Contents);
                }
                catch (XmlException)
                {
                    ModelState.AddModelError(
                        nameof(book.Contents),
                        "Contents должен содержать корректный XML.");
                }
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            

            var id = await _repository.CreateAsync(book);

            book.Id = id;

            return Ok(book);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] Book book)
        {
            if (id != book.Id)
            {
                return BadRequest("ID в URL и ID книги не совпадают.");
            }

            if (!string.IsNullOrWhiteSpace(book.Contents))
            {
                try
                {
                    XDocument.Parse(book.Contents);
                }
                catch (XmlException)
                {
                    ModelState.AddModelError(
                        nameof(book.Contents),
                        "Contents должен содержать корректный XML.");
                }
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.UpdateAsync(book);

            return Ok(book);
        }
    }
}
