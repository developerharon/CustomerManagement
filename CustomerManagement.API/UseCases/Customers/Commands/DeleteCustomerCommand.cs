using AutoMapper;
using CustomerManagement.API.Persistence;
using CustomerManagement.API.Shared.DTOs;
using CustomerManagement.API.Shared.DTOs.Customers;
using CustomerManagement.API.Shared.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.API.UseCases.Customers.Commands;

public class DeleteCustomerCommand : IRequest<ResponseDTO<CustomerDTO>>
{
    private readonly int Id;
    
    public DeleteCustomerCommand(int id)
    {
        Id = id;
    }

    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, ResponseDTO<CustomerDTO>>
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IMapper _mapper;

        public DeleteCustomerCommandHandler(ApplicationDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ResponseDTO<CustomerDTO>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _dbContext.CustomerEntities.Where(x => x.Id == request.Id).FirstOrDefaultAsync();

            if (customer == null)
                return ResponseDTO<CustomerDTO>.Create(ResponseType.Error, null, "Customer not found.");

            _dbContext.CustomerEntities.Remove(customer);
            await _dbContext.SaveChangesAsync();

            var responseDto = _mapper.Map<CustomerDTO>(customer);
            return ResponseDTO<CustomerDTO>.Create(ResponseType.Success, responseDto);
        }
    }
}