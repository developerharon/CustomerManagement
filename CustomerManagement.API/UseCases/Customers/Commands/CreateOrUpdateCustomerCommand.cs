using AutoMapper;
using CustomerManagement.API.Domain.Entities;
using CustomerManagement.API.Persistence;
using CustomerManagement.API.Shared.DTOs;
using CustomerManagement.API.Shared.DTOs.Customers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.API.UseCases.Customers.Commands;

public class CreateOrUpdateCustomerCommand : IRequest<ResponseDTO<CustomerDTO>>
{
    private readonly CreateOrUpdateCustomerDTO DTO;

    public CreateOrUpdateCustomerCommand(CreateOrUpdateCustomerDTO dto)
    {
        DTO = dto;
    }
    
    public class CreateOrUpdateCustomerCommandHandler : IRequestHandler<CreateOrUpdateCustomerCommand, ResponseDTO<CustomerDTO>>
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IMapper _mapper;

        public CreateOrUpdateCustomerCommandHandler(ApplicationDBContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task<ResponseDTO<CustomerDTO>> Handle(CreateOrUpdateCustomerCommand request,
            CancellationToken cancellationToken)
        {
            if (request.DTO.Id == 0)
            {
                return await CreateCustomerAsync(request);
            }

            var customer = await _dbContext.CustomerEntities.Where(x => x.Id == request.DTO.Id).FirstOrDefaultAsync();

            if (customer == null)
            {
                return ResponseDTO<CustomerDTO>.Create(Shared.Enums.ResponseType.Success, null, "Customer not found.");
            }

            customer = _mapper.Map(request.DTO, customer);
            _dbContext.CustomerEntities.Update(customer);
            await _dbContext.SaveChangesAsync();
            
            var responseDto =  _mapper.Map<CustomerDTO>(customer);
            return ResponseDTO<CustomerDTO>.Create(Shared.Enums.ResponseType.Success, responseDto);
        }

        private async Task<ResponseDTO<CustomerDTO>> CreateCustomerAsync(CreateOrUpdateCustomerCommand request)
        {
            var customer =  _mapper.Map<CustomerEntity>(request.DTO);
            await _dbContext.CustomerEntities.AddAsync(customer);
            await _dbContext.SaveChangesAsync();

            var responseDto = _mapper.Map<CustomerDTO>(customer);
            return ResponseDTO<CustomerDTO>.Create(Shared.Enums.ResponseType.Success, responseDto);
        }
    }
}