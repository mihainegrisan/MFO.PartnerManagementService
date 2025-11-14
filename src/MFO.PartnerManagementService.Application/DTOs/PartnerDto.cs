using MFO.PartnerManagementService.Domain.Entities;
using MFO.PartnerManagementService.Domain.Enums;

namespace MFO.PartnerManagementService.Application.DTOs;

public sealed record PartnerDto
{
    public required Guid PartnerId { get; init; }
    public required string LegalName { get; init; }
    public required string DisplayName { get; init; }
    public required PartnerType PartnerType { get; init; }

    public required OnboardingStatus OnboardingStatus { get; init; }
    public required bool IsLegalVerified { get; init; }


    public Guid? SellerId { get; init; }
    public Seller? Seller { get; init; }

    public Guid? SupplierId { get; init; }
    public Supplier? Supplier { get; init; }
}