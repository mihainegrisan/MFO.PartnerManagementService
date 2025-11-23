using MFO.PartnerManagementService.Application.Interfaces.Repositories;
using MFO.PartnerManagementService.Domain.Entities;
using MFO.PartnerManagementService.Infrastructure.Persistence;

namespace MFO.PartnerManagementService.Infrastructure.Repositories;

public class PartnerRepository : IPartnerRepository
{
    private readonly PartnerManagementDbContext _db;

    public PartnerRepository(PartnerManagementDbContext db)
    {
        _db = db;
    }
    
    public async Task<Partner> UpdatePartnerAsync(Partner partner, CancellationToken cancellationToken)
    {
        _db.Partners.Update(partner);
        await _db.SaveChangesAsync(cancellationToken);
        return partner;
    }

    public async Task AddPartnerAsync(Partner partner, CancellationToken cancellationToken)
    {
        await _db.Partners.AddAsync(partner, cancellationToken);
    }
}