using AutoMapper;
using FluentResults;
using MediatR;
using MFO.PartnerManagementService.Application.DTOs;
using MFO.PartnerManagementService.Application.Interfaces.Repositories;
using MFO.PartnerManagementService.Domain.Errors;

namespace MFO.PartnerManagementService.Application.Features.Queries;

public sealed record GetPartnerDetailsForValidationQuery(Guid PartnerId) : IRequest<Result<PartnerValidationDetailsDto>>;

public class GetPartnerDetailsForValidationQueryHandler : IRequestHandler<GetPartnerDetailsForValidationQuery, Result<PartnerValidationDetailsDto>>
{
    private readonly IPartnerQueryRepository _partnerQueryRepository;
    private readonly IMapper _mapper;

    public GetPartnerDetailsForValidationQueryHandler(IPartnerQueryRepository partnerQueryRepository, IMapper mapper)
    {
        _partnerQueryRepository = partnerQueryRepository;
        _mapper = mapper;
    }

    public async Task<Result<PartnerValidationDetailsDto>> Handle(
        GetPartnerDetailsForValidationQuery request,
        CancellationToken cancellationToken)
    {
        var partner = await _partnerQueryRepository.GetPartnerByIdAsync(request.PartnerId, cancellationToken);

        if (partner is null)
        {
            return Result.Fail<PartnerValidationDetailsDto>(
                new NotFoundError($"Partner details for ID {request.PartnerId} were not found.")
            );
        }

        var dto = _mapper.Map<PartnerValidationDetailsDto>(partner);

        return Result.Ok(dto);
    }
}