using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    protected readonly IConfiguration _configuration;

    public ApplicationDbContext(IConfiguration configuration)
    {
        _configuration = configuration;

        // var folder = Environment.SpecialFolder.LocalApplicationData;
        // var path = Environment.GetFolderPath(folder);
        // DbPath = System.IO.Path.Join(path, "blogging.db");
    }

    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    // public string DbPath { get; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // optionsBuilder.UseSqlite($"Data Source={DbPath}");
        optionsBuilder.UseSqlite(_configuration.GetConnectionString("DefaultConnection"));

        // Register the entity sets needed by OpenIddict.
        // Note: use the generic overload if you need to replace the default OpenIddict entities.
        optionsBuilder.UseOpenIddict();
    }
}

public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }

    public List<Post> Posts { get; } = new();
}

public class Post
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public int BlogId { get; set; }
    public Blog Blog { get; set; }
}