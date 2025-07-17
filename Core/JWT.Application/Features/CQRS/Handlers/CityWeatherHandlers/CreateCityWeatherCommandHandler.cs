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
    public class CreateCityWeatherCommandHandler
    {
        private readonly IRepository<CityWeather> _repository;

        public CreateCityWeatherCommandHandler(IRepository<CityWeather> repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateCityWeatherCommand command)
        {
            await _repository.CreateAsync(new CityWeather
            {
               Name = command.Name,
               Date = command.Date,
               Temperature = command.Temperature,
            });
        }

    }
}
