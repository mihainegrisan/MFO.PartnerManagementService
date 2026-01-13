using MFO.PartnerManagementService.Domain.Enums;

namespace MFO.PartnerManagementService.Application.DTOs;

public sealed record PartnerValidationDetailsDto
{
    public required Guid PartnerId { get; init; }
    public required string DisplayName { get; init; }
    public required OnboardingStatus OnboardingStatus { get; init; }
    public required SellerStatus SellerStatus { get; init; }
    public required Guid DefaultWarehouseId { get; init; }
}