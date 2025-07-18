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
    public class RemoveDistrictCommandHandlers
    {
        private readonly IRepository<District> _repository;
        public RemoveDistrictCommandHandlers(IRepository<District> repository)
        {
            _repository = repository;
        }
        public async Task Handle(RemoveDistrictCommand command)
        {
            var value = await _repository.GetByIdAsync(command.Id);
            await _repository.RemoveAsync(value);
        }

    }
}
