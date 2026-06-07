using Microsoft.EntityFrameworkCore;
using ComicCrazy.API.Models;


namespace ComicCrazy.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Comic> Comics => Set<Comic>();
    public DbSet<CollectionItem> CollectionItems => Set<CollectionItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Each user can only have one entry per comic in their collection
        modelBuilder.Entity<CollectionItem>()
            .HasIndex(c => new { c.UserId, c.ComicId })
            .IsUnique();

        // Store the enum as a string so the database is readable
        modelBuilder.Entity<CollectionItem>()
            .Property(c => c.Status)
            .HasConversion<string>();

        // Each email can only be registered once
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
    }
}