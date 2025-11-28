using FluentResults;
using MediatR;
using MFO.PartnerManagementService.Application.DTOs;
using MFO.PartnerManagementService.Application.Events;
using MFO.PartnerManagementService.Application.Interfaces;
using MFO.PartnerManagementService.Application.Interfaces.Repositories;
using MFO.PartnerManagementService.Domain.Enums;
using MFO.PartnerManagementService.Domain.Errors;

namespace MFO.PartnerManagementService.Application.Features.Commands;

public sealed record UpdateSellerStatusCommand(Guid PartnerId, SellerStatus NewStatus) : IRequest<Result<UpdateSellerStatusResponse>>;

public class UpdateSellerStatusCommandHandler : IRequestHandler<UpdateSellerStatusCommand, Result<UpdateSellerStatusResponse>>
{
    private readonly IPartnerRepository _partnerRepository;
    private readonly IPartnerQueryRepository _partnerQueryRepository;
    private readonly IPartnerManagementPublisher _publisher;

    public UpdateSellerStatusCommandHandler(
        IPartnerRepository partnerRepository,
        IPartnerQueryRepository partnerQueryRepository,
        IPartnerManagementPublisher publisher)
    {
        _partnerRepository = partnerRepository;
        _partnerQueryRepository = partnerQueryRepository;
        _publisher = publisher;
    }

    public async Task<Result<UpdateSellerStatusResponse>> Handle(
        UpdateSellerStatusCommand request,
        CancellationToken cancellationToken)
    {
        var partner = await _partnerQueryRepository.GetPartnerByIdAsync(request.PartnerId, cancellationToken);

        if (partner is null)
        {
            return Result.Fail<UpdateSellerStatusResponse>(
                new NotFoundError($"Partner details for ID {request.PartnerId} were not found.")
            );
        }

        if (partner.Seller is null)
        {
            return Result.Fail<UpdateSellerStatusResponse>(
                new Error($"Partner ID {request.PartnerId} is not configured as a Seller. Status update aborted.")
                    .WithMetadata("ErrorCode", "NotASeller")
            );
        }

        partner.Seller.SellerStatus = request.NewStatus;
        await _partnerRepository.UpdatePartnerAsync(partner, cancellationToken);

        var sellerStatusUpdatedEvent = new SellerStatusUpdatedEvent(
            partner.PartnerId,
            request.NewStatus,
            DateTimeOffset.UtcNow
        );

        await _publisher.PublishSellerStatusUpdatedEvent(sellerStatusUpdatedEvent);

        return new UpdateSellerStatusResponse
        {
            PartnerId = request.PartnerId,
            Message = "Seller status updated successfully.",
            NewStatus = request.NewStatus.ToString()
        };
    }
}