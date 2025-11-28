using FluentValidation;

namespace MFO.PartnerManagementService.Application.Features.Commands;

public class UpdateSellerStatusCommandValidator : AbstractValidator<UpdateSellerStatusCommand>
{
    public UpdateSellerStatusCommandValidator()
    {
        RuleFor(c => c.PartnerId)
            .NotNull()
            .NotEmpty();

        RuleFor(c => c.NewStatus)
            .NotNull();
    }
}
