using System.Net.Mime;
using System.Text;
using AspNetCoreMySQL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreMySQL.Controllers;

[Produces(MediaTypeNames.Application.Json)]
[Route("[controller]")]
public class TestController : MyControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    public TestController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult Create()
    {
        InsertData();
        return Ok();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult Get()
    {
        PrintData();
        var planets = _dbContext.Book.ToList();

        return Ok(planets);
    }

    private void InsertData()
    {
        // Creates the database if not exists
        _dbContext.Database.EnsureCreated();

        // Adds a publisher
        var publisher = new Publisher
        {
            Name = "Mariner Books"
        };
        _dbContext.Publisher.Add(publisher);

        // Adds some books
        _dbContext.Book.Add(new Book
        {
            ISBN = "978-0544003415",
            Title = "The Lord of the Rings",
            Author = "J.R.R. Tolkien",
            Language = "English",
            Pages = 1216,
            Publisher = publisher
        });
        _dbContext.Book.Add(new Book
        {
            ISBN = "978-0547247762",
            Title = "The Sealed Letter",
            Author = "Emma Donoghue",
            Language = "English",
            Pages = 416,
            Publisher = publisher
        });

        // Saves changes
        _dbContext.SaveChanges();
    }

    private void PrintData()
    {
        // Gets and prints all books in database
        var books = _dbContext.Book
              .Include(p => p.Publisher);
        foreach (var book in books)
        {
            var data = new StringBuilder();
            data.AppendLine($"ISBN: {book.ISBN}");
            data.AppendLine($"Title: {book.Title}");
            data.AppendLine($"Publisher: {book.Publisher.Name}");
            Console.WriteLine(data.ToString());
        }
    }
}