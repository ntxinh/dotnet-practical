using System;
using LibraryApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        { }

        public DbSet<Book> Books { get; set; }
    }
}
