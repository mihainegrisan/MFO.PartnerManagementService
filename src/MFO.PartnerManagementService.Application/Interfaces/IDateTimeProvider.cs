namespace MFO.PartnerManagementService.Application.Interfaces;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}