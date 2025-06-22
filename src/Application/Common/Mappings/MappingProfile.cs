using Application.Common.DTOs.Customers;
using Application.Common.DTOs.Inventory;
using Application.Common.DTOs.Invoices;
using Application.Common.DTOs.ServiceOrders;
using Application.Common.DTOs.Spares;
using Application.Common.DTOs.Users;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User mappings
        CreateMap<User, UserDto>()
            .ForMember(d => d.Roles, o => o.MapFrom(s => s.UserRoles.Select(ur => ur.Role)))
            .ForMember(d => d.Specializations, o => o.MapFrom(s => s.UserSpecializations.Select(us => us.Specialization)));
        CreateMap<Role, RoleDto>();
        CreateMap<Specialization, SpecializationDto>();

        // Customer mappings
        CreateMap<Customer, CustomerDto>()
            .ForMember(d => d.Vehicles, o => o.MapFrom(s => s.Vehicles));
        CreateMap<Vehicle, VehicleDto>()
            .ForMember(d => d.Owner, o => o.MapFrom(s => s.Customer));

        // Service Order mappings
        CreateMap<ServiceOrder, ServiceOrderDto>()
            .ForMember(d => d.Vehicle, o => o.MapFrom(s => s.Vehicle))
            .ForMember(d => d.Mechanic, o => o.MapFrom(s => s.Mechanic))
            .ForMember(d => d.ServiceType, o => o.MapFrom(s => s.ServiceType))
            .ForMember(d => d.Status, o => o.MapFrom(s => s.Status))
            .ForMember(d => d.DiagnosticDetails, o => o.MapFrom(s => s.DiagnosticDetails))
            .ForMember(d => d.OrderDetails, o => o.MapFrom(s => s.OrderDetails));
        CreateMap<ServiceType, ServiceTypeDto>();
        CreateMap<Status, StatusDto>();
        CreateMap<DiagnosticDetail, DiagnosticDetailDto>();
        CreateMap<OrderDetail, OrderDetailDto>()
            .ForMember(d => d.Spare, o => o.MapFrom(s => s.Spare))
            .ForMember(d => d.Subtotal, o => o.MapFrom(s => s.Total));

        // Inventory mappings
        CreateMap<Inventory, InventoryDto>()
            .ForMember(d => d.Details, o => o.MapFrom(s => s.InventoryDetails));
        CreateMap<InventoryDetail, InventoryDetailDto>();
        CreateMap<Spare, SpareDto>();

        // Invoice mappings
        CreateMap<Invoice, InvoiceDto>()
            .ForMember(d => d.ServiceOrder, o => o.MapFrom(s => s.ServiceOrder));
    }
} 