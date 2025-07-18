using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JWT.Application.Features.CQRS.Commands.CityWeatherCommands;
using JWT.Application.Interfaces;
using JWT.Domain.Entity;

namespace JWT.Application.Features.CQRS.Handlers.CityWeatherHandlers
{
    public class UpdateCityWeatherCommandHandler
    {

        private readonly IRepository<CityWeather> _repository;

        public UpdateCityWeatherCommandHandler(IRepository<CityWeather> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateCityWeatherCommand command)
        {
            var values = await _repository.GetByIdAsync(command.CityId);
           values.Name = command.Name;
            values.Date = command.Date;
            values.Temperature = command.Temperature;

            await _repository.UpdateAsync(values);

        }
    }
}
