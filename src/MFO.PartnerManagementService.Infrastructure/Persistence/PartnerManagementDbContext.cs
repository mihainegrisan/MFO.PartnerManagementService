using MFO.PartnerManagementService.Application.Interfaces;
using MFO.PartnerManagementService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MFO.PartnerManagementService.Infrastructure.Persistence;

public class PartnerManagementDbContext : AuditableDbContextBase
{
    public PartnerManagementDbContext(
        DbContextOptions<PartnerManagementDbContext> options,
        IDateTimeProvider dateTimeProvider,
        IUserContextProvider userContextProvider) 
        : base(options, dateTimeProvider, userContextProvider)
    {
    }

    public DbSet<Partner> Partners { get; set; }
    public DbSet<Seller> Sellers { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }

    public override int SaveChanges()
    {
        ApplyAuditing();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyAuditing();
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Seller>(entity =>
        {
            // 1. Configure the primary key (equivalent to [Key])
            entity.HasKey(e => e.PartnerId);

            // 2. Prevent the database from generating the value (equivalent to [DatabaseGenerated(DatabaseGeneratedOption.None)])
            entity.Property(e => e.PartnerId)
                  .ValueGeneratedNever();

            // Note: The 'required' keyword in C# 11+ is handled automatically by EF Core
            // if the property is non-nullable (like Guid).
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            // 1. Configure the primary key (equivalent to [Key])
            entity.HasKey(e => e.PartnerId);

            // 2. Prevent the database from generating the value (equivalent to [DatabaseGenerated(DatabaseGeneratedOption.None)])
            entity.Property(e => e.PartnerId)
                  .ValueGeneratedNever();
        });

        base.OnModelCreating(modelBuilder);
    }
}