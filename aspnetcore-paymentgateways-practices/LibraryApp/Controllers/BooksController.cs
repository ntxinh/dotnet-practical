using System;
using Microsoft.AspNetCore.Mvc;
using LibraryApp.Data.Services;

namespace LibraryApp.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _service;

        public BooksController(IBookService service)
        {
            _service = service;
        }

        // GET: Books
        public IActionResult Index()
        {
            var items = _service.GetAll();
            return View(items);
        }

        // GET: Books/Details/5
        public IActionResult Details(Guid id)
        {
            var book = _service.GetById(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }
    }
}
