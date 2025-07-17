using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JWT.Application.Features.CQRS.Results.CityWeatherResults;
using JWT.Application.Interfaces;
using JWT.Domain.Entity;

namespace JWT.Application.Features.CQRS.Handlers.CityWeatherHandlers
{
    public class GetCityWeatherQueryHandler
    {
        private readonly IRepository<CityWeather> _repository;

        public GetCityWeatherQueryHandler(IRepository<CityWeather> repository)
        {
            _repository = repository;
        }
        public async Task<List<GetCityWeatherQueryResult>> Handle()
        {
            var values = await _repository.GetAllAsync();
            return values.Select(x => new GetCityWeatherQueryResult
            {
              CityId = x.CityId,
              Date = x.Date,
              Name = x.Name,
              Temperature=x.Temperature,
              
            }).ToList();
        }



    }
}

