using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder) { }
}

// public class ApplicationUser : IdentityUser { }