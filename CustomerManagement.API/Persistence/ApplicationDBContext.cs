using CustomerManagement.API.Domain.Entities;
using CustomerManagement.API.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.API.Persistence;

public class ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : DbContext(options)
{
    public DbSet<CustomerEntity> CustomerEntities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CustomerEntity>()
            .HasIndex(i => i.Email)
            .IsUnique();
    }

    public override int SaveChanges()
    {
        UpdateAuditFields();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        UpdateAuditFields();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateAuditFields()
    {
        var tracker = ChangeTracker;

        foreach (var entry in tracker.Entries())
        {
            if (entry.Entity is BaseEntity referenceEntity)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        referenceEntity.CreatedAt = DateTimeOffset.UtcNow;
                        break;
                    case EntityState.Modified:
                        referenceEntity.UpdatedAt = DateTimeOffset.UtcNow;
                        break;
                }
            }
        }
    }
}