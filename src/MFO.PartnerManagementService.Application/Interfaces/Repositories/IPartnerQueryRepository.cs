using MFO.PartnerManagementService.Domain.Entities;

namespace MFO.PartnerManagementService.Application.Interfaces.Repositories;

public interface IPartnerQueryRepository
{
    Task<Partner?> GetPartnerByIdAsync(Guid partnerId, CancellationToken cancellationToken);
}