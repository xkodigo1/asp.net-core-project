using AutoMapper;
using Domain.Entities;
using Application.Common.DTOs.ServiceTypes;

namespace Application.Common.Mappings;

public class ServiceTypeMappingProfile : Profile
{
    public ServiceTypeMappingProfile()
    {
        CreateMap<ServiceType, ServiceTypeDto>();
        CreateMap<CreateServiceTypeDto, ServiceType>();
        CreateMap<UpdateServiceTypeDto, ServiceType>();
    }
} 