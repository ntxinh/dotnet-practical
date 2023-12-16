using Microsoft.EntityFrameworkCore;

namespace AspNetCorePostgres;

public class ApplicationDbContext : DbContext
{
    protected readonly IConfiguration _configuration;

    public ApplicationDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbSet<Account> Accounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("BloggingContext"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(x => x.UserId);
            entity.ToTable("account");
            entity.Property(x => x.UserId)
                .HasColumnName("userid");
            entity.Property(x => x.Username)
                .HasColumnName("username");
            entity.Property(x => x.Password)
                .HasColumnName("password");
            entity.Property(x => x.Email)
                .HasColumnName("email");
        });
    }
}

public class Account
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}