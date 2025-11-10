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
}