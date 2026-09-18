using Books.Mvc.Interfaces;
using Books.Mvc.Models;
using Books.Mvc.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Xml;
using System.Xml.Linq;

namespace Books.Mvc.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookRepository _repository;
        private readonly IBookService _service;

        public BooksController(
            IBookRepository repository,
            IBookService service)
        {
            _repository = repository;
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _repository.GetAllAsync();

            return View(books);
        }

        
        public async Task<IActionResult> Details(int id)
        {
            var book = await _service.GetDetailsAsync(id);

            if (book == null)
                return NotFound();

            return View(book);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
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
                return View(book);

            await _repository.CreateAsync(book);

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _repository.GetByIdAsync(id);

            if (book == null)
                return NotFound();

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Book book)
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
                return View(book);

            await _repository.UpdateAsync(book);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(
    Duration = 0,
    Location = ResponseCacheLocation.None,
    NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id
                    ?? HttpContext.TraceIdentifier
            });
        }
    }
}
