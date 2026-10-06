using AutoMapper;
using CustomerManagement.API.Persistence;
using CustomerManagement.API.Shared.DTOs;
using CustomerManagement.API.Shared.DTOs.Customers;
using CustomerManagement.API.Shared.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.API.UseCases.Customers.Queries;

public class GetAllCustomersQuery : IRequest<ResponseDTO<IEnumerable<CustomerDTO>>>
{
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, ResponseDTO<IEnumerable<CustomerDTO>>>
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllCustomersQueryHandler(ApplicationDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        public async Task<ResponseDTO<IEnumerable<CustomerDTO>>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await _dbContext.CustomerEntities.ToListAsync();

            var customerDtos = _mapper.Map<IEnumerable<CustomerDTO>>(customers);
            return ResponseDTO<IEnumerable<CustomerDTO>>.Create(ResponseType.Success, customerDtos);
        }
    }
}