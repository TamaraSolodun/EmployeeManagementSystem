using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Office> Offices { get; set; }
    public DbSet<Role> Roles { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Office>(entity => entity
        .HasMany(u => u.Employees)
        .WithOne(t => t.Office)
        .HasForeignKey(t => t.OfficeId)
        .OnDelete(DeleteBehavior.SetNull));

        modelBuilder.Entity<Role>(entity => entity
        .HasMany(u => u.Employees)
        .WithOne(t => t.Role)
        .HasForeignKey(t => t.RoleId)
        .OnDelete(DeleteBehavior.Restrict));
    }
}