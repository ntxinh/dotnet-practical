using System;
using System.Collections.Generic;
using System.Linq;
using LibraryApp.Data.Models;

namespace LibraryApp.Data.Services
{
    public class BookService : IBookService
    {
        private readonly AppDbContext _context;
        public BookService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Book> GetAll()
        {
            var items = _context.Books.ToList();
            return items;
        }

        public Book GetById(Guid courseId) => _context.Books.FirstOrDefault(n => n.Id == courseId);
    }
}
