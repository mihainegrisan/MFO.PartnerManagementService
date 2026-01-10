using MFO.PartnerManagementService.Domain.Enums;

namespace MFO.PartnerManagementService.Application.DTOs;

public class CreateNewPartnerDto
{
    public required string LegalName { get; init; }
    public required string DisplayName { get; init; }
    public required PartnerType PartnerType { get; init; }

    public Guid? SellerId { get; init; }
    public Guid? SupplierId { get; init; }
}