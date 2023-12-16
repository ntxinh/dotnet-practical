using System;
using System.Collections.Generic;
using LibraryApp.Data.Models;

namespace LibraryApp.Data.Services
{
    public interface IBookService
    {
        IEnumerable<Book> GetAll();
        Book GetById(Guid courseId);
    }
}
