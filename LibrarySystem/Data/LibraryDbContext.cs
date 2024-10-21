using LibrarySystem.Configurations;
using LibrarySystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Data;

public class LibraryDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; } = null!;

    public DbSet<Book> Books { get; set; } = null!;

    public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
        :base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CategoryConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
