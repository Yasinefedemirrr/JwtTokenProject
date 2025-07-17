using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JWT.Application.Features.CQRS.Queries.CityWeatherQueries;
using JWT.Application.Features.CQRS.Results.CityWeatherResults;
using JWT.Application.Interfaces;
using JWT.Domain.Entity;

namespace JWT.Application.Features.CQRS.Handlers.CityWeatherHandlers
{
    public class GetCityWeatherByIdQueryHandler
    {
        private readonly IRepository<CityWeather> _repository;

        public GetCityWeatherByIdQueryHandler(IRepository<CityWeather> repository)
        {
            _repository = repository;
        }
        public async Task<GetCityWeatherByIdQueryResult> Handle(GetCityWeatherByIdQuery query)
        {
            var values = await _repository.GetByIdAsync(query.Id);
            return new GetCityWeatherByIdQueryResult
            {
               CityId = values.CityId,
               Temperature = values.Temperature,
               Name = values.Name,
               Date= values.Date,

            };
        }


    }
}
