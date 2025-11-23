using MFO.PartnerManagementService.Domain.Entities;

namespace MFO.PartnerManagementService.Application.Interfaces.Repositories;

public interface IPartnerRepository
{
    Task<Partner> UpdatePartnerAsync(Partner partner, CancellationToken cancellationToken);
    Task AddPartnerAsync(Partner partner, CancellationToken cancellationToken);
}