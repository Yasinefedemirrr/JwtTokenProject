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
    public class RemoveCityWeatherCommandHandler
    {
        private readonly IRepository<CityWeather> _repository;
public RemoveCityWeatherCommandHandler(IRepository<CityWeather> repository)
        {
            _repository = repository;
        }
        public async Task Handle(RemoveCityWeatherCommand command)
        {
            var value =await _repository.GetByIdAsync(command.Id);
            await _repository.RemoveAsync(value);
        }

    }
}
