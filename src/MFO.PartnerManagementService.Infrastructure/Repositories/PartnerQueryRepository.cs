using MFO.PartnerManagementService.Application.Interfaces.Repositories;
using MFO.PartnerManagementService.Domain.Entities;
using MFO.PartnerManagementService.Infrastructure.Persistence;

namespace MFO.PartnerManagementService.Infrastructure.Repositories;

public class PartnerQueryRepository : IPartnerQueryRepository
{
    private readonly PartnerManagementDbContext _db;

    public PartnerQueryRepository(PartnerManagementDbContext db)
    {
        _db = db;
    }

    public async Task<Partner?> GetPartnerByIdAsync(Guid partnerId, CancellationToken cancellationToken)
        => await _db.Partners.FindAsync([partnerId], cancellationToken);
}
