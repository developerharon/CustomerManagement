using AutoMapper;
using CustomerManagement.API.Domain.Entities;
using CustomerManagement.API.Shared.DTOs.Customers;

namespace CustomerManagement.API.Mappings;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<CreateOrUpdateCustomerDTO, CustomerEntity>().ReverseMap();
        CreateMap<CustomerEntity, CustomerDTO>().ReverseMap();
    }
}