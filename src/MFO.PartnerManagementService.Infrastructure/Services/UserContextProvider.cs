using MFO.PartnerManagementService.Application.Interfaces;

namespace MFO.PartnerManagementService.Infrastructure.Services;

public class UserContextProvider : IUserContextProvider
{
    public string? UserId => "system";
}