using AutoMapper;
using MFO.PartnerManagementService.Application.DTOs;
using MFO.PartnerManagementService.Domain.Entities;

namespace MFO.PartnerManagementService.Application.Mapping;

public class PartnerManagementServiceProfile : Profile
{
    public PartnerManagementServiceProfile()
    {
        #region Map Domain entities to DTOs

        CreateMap<Partner, PartnerDto>();
        
        // CreateMap<Seller, SellerDto>();
        // CreateMap<Supplier, SupplierDto>();

        #endregion

        #region Map Domain entities to light DTOs

        CreateMap<Seller, SellerLightDto>();
        CreateMap<Supplier, SupplierLightDto>();

        #endregion


    }
}