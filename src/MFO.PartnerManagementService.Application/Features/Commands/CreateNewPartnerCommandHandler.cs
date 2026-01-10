using AutoMapper;
using FluentResults;
using MediatR;
using MFO.PartnerManagementService.Application.DTOs;
using MFO.PartnerManagementService.Application.Interfaces;
using MFO.PartnerManagementService.Application.Interfaces.Repositories;
using MFO.PartnerManagementService.Domain.Entities;
using MFO.PartnerManagementService.Domain.Enums;

namespace MFO.PartnerManagementService.Application.Features.Commands;

public sealed record CreateNewPartnerCommand(CreateNewPartnerDto CreateNewPartnerDto) : IRequest<Result>;

public class CreateNewPartnerCommandHandler : IRequestHandler<CreateNewPartnerCommand, Result>
{
    private readonly IPartnerRepository _partnerRepository;
    private readonly IMapper _mapper;
    private readonly IPartnerManagementPublisher _publisher;

    public CreateNewPartnerCommandHandler(IPartnerRepository partnerRepository, IMapper mapper, IPartnerManagementPublisher publisher)
    {
        _partnerRepository = partnerRepository;
        _mapper = mapper;
        _publisher = publisher;
    }

    public async Task<Result> Handle(CreateNewPartnerCommand request, CancellationToken cancellationToken)
    {
        var newPartnerDto = request.CreateNewPartnerDto;

        if (!newPartnerDto.SellerId.HasValue && !newPartnerDto.SupplierId.HasValue)
        {
            return Result.Fail(
                new Error("A Partner must be associated with either a Seller or a Supplier ID.")
                    .WithMetadata("ErrorCode", "PartnerMustBeSellerOrSupplier")
            );
        }

        // 2. Validate Existence of Referenced Entities
        var failureList = new List<IError>();

        //if (newPartnerDto.PartnerId.HasValue && !await _sellerRepository.SellerExistsAsync(newPartnerDto.PartnerId.Value))
        //{
        //    failureList.Add(new Error($"Seller ID {newPartnerDto.PartnerId.Value} not found."));
        //}

        //if (newPartnerDto.SupplierId.HasValue && !await _supplierRepository.SupplierExistsAsync(newPartnerDto.SupplierId.Value))
        //{
        //    failureList.Add(new Error($"Supplier ID {newPartnerDto.SupplierId.Value} not found."));
        //}

        if (failureList.Any())
        {
            return Result.Fail(failureList);
        }

        var partner = _mapper.Map<Partner>(newPartnerDto);

        partner.OnboardingStatus = OnboardingStatus.AwaitingVerification;
        partner.IsLegalVerified = false;

        await _partnerRepository.AddPartnerAsync(partner, cancellationToken);

        // 4. **Publish Domain Events (Optional but Recommended)**
        // if (partner.DomainEvents.Any()) { /* publish events */ }

        return Result.Ok().WithSuccess($"Partner with ID {partner.PartnerId} created successfully.");
    }
}