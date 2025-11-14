namespace MFO.PartnerManagementService.Application.DTOs;

public sealed record SupplierLightDto
{
    public required string PrimaryServiceProvided { get; init; }
    public required decimal PerformanceScore { get; init; }

    public required Guid PartnerId { get; init; }
}