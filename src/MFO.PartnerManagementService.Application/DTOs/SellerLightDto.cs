using MFO.PartnerManagementService.Domain.Enums;

namespace MFO.PartnerManagementService.Application.DTOs;

public sealed record SellerLightDto
{
    public required SellerStatus SellerStatus { get; init; }

    public required Guid DefaultWarehouseId { get; init; }

    public required Guid PartnerId { get; init; }
}