namespace MFO.PartnerManagementService.Application.DTOs;

public sealed record UpdateSellerStatusResponse
{
    public required Guid PartnerId { get; init; }
    public required string Message { get; init; }
    public required string NewStatus { get; init; }
}