using MFO.PartnerManagementService.Application.Interfaces;

namespace MFO.PartnerManagementService.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}