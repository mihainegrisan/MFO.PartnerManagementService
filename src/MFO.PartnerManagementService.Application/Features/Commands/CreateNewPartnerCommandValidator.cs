using FluentValidation;

namespace MFO.PartnerManagementService.Application.Features.Commands;

public class CreateNewPartnerCommandValidator : AbstractValidator<CreateNewPartnerCommand>
{
    public CreateNewPartnerCommandValidator()
    {
        RuleFor(c => c.CreateNewPartnerDto.DisplayName)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(c => c.CreateNewPartnerDto.LegalName)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(c => c.CreateNewPartnerDto.PartnerType)
            .NotNull()
            .NotEmpty();

        //RuleFor(c => c.CreateNewPartnerDto.SellerId)
        //RuleFor(c => c.CreateNewPartnerDto.SupplierId)
    }
}