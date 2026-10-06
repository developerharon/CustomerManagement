using AutoMapper;
using CustomerManagement.API.Persistence;
using CustomerManagement.API.Shared.DTOs;
using CustomerManagement.API.Shared.DTOs.Customers;
using CustomerManagement.API.Shared.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.API.UseCases.Customers.Queries;

public class GetCustomerByIdQuery : IRequest<ResponseDTO<CustomerDTO>>
{
    private readonly int Id;

    public GetCustomerByIdQuery(int id) => Id = id;
    
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, ResponseDTO<CustomerDTO>>
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IMapper _mapper;

        public GetCustomerByIdQueryHandler(ApplicationDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        public async Task<ResponseDTO<CustomerDTO>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _dbContext.CustomerEntities.Where(x => x.Id == request.Id).FirstOrDefaultAsync();

            if (customer == null)
                return ResponseDTO<CustomerDTO>.Create(ResponseType.Error, null, "Customer not found.");

            var responseDto = _mapper.Map<CustomerDTO>(customer);
            return ResponseDTO<CustomerDTO>.Create(ResponseType.Success, responseDto);
        }
    }
}