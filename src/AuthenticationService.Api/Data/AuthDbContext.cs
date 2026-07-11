using AuthenticationService.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Api.Data;

public sealed class AuthDbContext(DbContextOptions<AuthDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var user = modelBuilder.Entity<User>();

        user.ToTable("Users");
        user.HasKey(entity => entity.Id);
        user.Property(entity => entity.Email).HasMaxLength(320).IsRequired();
        user.Property(entity => entity.NormalizedEmail).HasMaxLength(320).IsRequired();
        user.Property(entity => entity.PasswordHash).HasMaxLength(100).IsRequired();
        user.HasIndex(entity => entity.NormalizedEmail).IsUnique();
    }
}
