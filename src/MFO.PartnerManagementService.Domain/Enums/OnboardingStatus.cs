namespace MFO.PartnerManagementService.Domain.Enums;

public enum OnboardingStatus
{
    Draft = 1,
    AwaitingVerification = 2,
    Rejected = 3,
    // Fully reviewed and approved by the marketplace.
    Onboarded = 4
}
