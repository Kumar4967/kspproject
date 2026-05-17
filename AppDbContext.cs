using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Advertisement> Ads { get; set; } = null!;
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
}

public class Advertisement
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
}
