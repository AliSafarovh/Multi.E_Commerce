using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers;

public class CreateAddressCommandHandler
{
    private readonly IRepository<Address> _repository;

    public CreateAddressCommandHandler(IRepository<Address> repository)
    {
        _repository = repository;
    }

    public async Task Handle(CreateAddressCommand createAddressComand)
    {
        await _repository.CreateAsync(new Address
        {
            City = createAddressComand.City,
            Detail = createAddressComand.Detail,
            District = createAddressComand.District,
            UserId = createAddressComand.UserId
        });
    }
}
