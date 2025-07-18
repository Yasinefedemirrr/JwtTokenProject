using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JWT.Application.Features.CQRS.Queries.CityWeatherQueries;
using JWT.Application.Features.CQRS.Queries.DistrictQueries;
using JWT.Application.Features.CQRS.Results.CityWeatherResults;
using JWT.Application.Features.CQRS.Results.DistrictResults;
using JWT.Application.Interfaces;
using JWT.Domain.Entity;

namespace JWT.Application.Features.CQRS.Handlers.Districthandlers
{
    public class GetDistrictByIdQueryHandler
    {
        private readonly IRepository<District> _repository;

        public GetDistrictByIdQueryHandler(IRepository<District> repository)
        {
            _repository = repository;
        }

        public async Task<GetDistrictByIdQueryResult> Handle(GetDistrictByIdQuery query)
        {
            var values = await _repository.GetByIdAsync(query.Id);
            return new GetDistrictByIdQueryResult
            {
                DistrictId = values.DistrictId,
                Temperature = values.Temperature,
                DistrictName = values.DistrictName,
                Date = values.Date,
                CityId=values.CityId,

            };
        }




    }
}
