using FluentValidation;

namespace MFO.PartnerManagementService.Application.Features.Queries;

public class GetPartnerDetailsForValidationQueryValidator : AbstractValidator<GetPartnerDetailsForValidationQuery>
{
    public GetPartnerDetailsForValidationQueryValidator()
    {
        RuleFor(q => q.PartnerId)
            .NotNull()
            .NotEmpty();
    }
}