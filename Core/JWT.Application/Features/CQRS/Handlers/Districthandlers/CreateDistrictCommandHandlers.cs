using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JWT.Application.Features.CQRS.Commands.CityWeatherCommands;
using JWT.Application.Features.CQRS.Commands.DistrictCommands;
using JWT.Application.Interfaces;
using JWT.Domain.Entity;

namespace JWT.Application.Features.CQRS.Handlers.Districthandlers
{
    public class CreateDistrictCommandHandlers
    {
        private readonly IRepository<District> _repository;

        public CreateDistrictCommandHandlers(IRepository<District> repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateDistrictCommand command)
        {
            await _repository.CreateAsync(new District
            {
               DistrictName = command.DistrictName,
                Date = command.Date,
                Temperature = command.Temperature,
                CityId = command.CityId,
                
            });
        }

    }
}
