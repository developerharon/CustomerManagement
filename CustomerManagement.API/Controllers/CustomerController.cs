using CustomerManagement.API.Shared.DTOs.Customers;
using CustomerManagement.API.Shared.Enums;
using CustomerManagement.API.UseCases.Customers.Commands;
using CustomerManagement.API.UseCases.Customers.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _mediator.Send(new GetAllCustomersQuery());

        if (result.ResponseType == ResponseType.Success)
            return Ok(result.Data);
        return BadRequest(result.Message);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _mediator.Send(new GetCustomerByIdQuery(id));
        
        if (result.ResponseType == ResponseType.Success)
            return Ok(result.Data);
        return NotFound(result.Message);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateOrUpdateCustomerDTO dto)
    {
        var result = await _mediator.Send(new CreateOrUpdateCustomerCommand(dto));

        if (result.ResponseType == ResponseType.Success)
            return Ok(result.Data);

        return BadRequest((result.Message));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, CreateOrUpdateCustomerDTO dto)
    {
        var result = await _mediator.Send(new CreateOrUpdateCustomerCommand(dto));

        if (result.ResponseType == ResponseType.Success)
            return Ok(result.Data);

        return BadRequest(result.Message);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteCustomerCommand(id));
        
        if (result.ResponseType == ResponseType.Success)
            return Ok(result.Data);
        
        return NotFound(result.Message);
    }
}