using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JWT.Application.Features.CQRS.Results.CityWeatherResults;
using JWT.Application.Features.CQRS.Results.DistrictResults;
using JWT.Application.Interfaces;
using JWT.Domain.Entity;

namespace JWT.Application.Features.CQRS.Handlers.Districthandlers
{
    public class GetDistrictQueryHandler
    {
        private readonly IRepository<District> _repository;

        public GetDistrictQueryHandler(IRepository<District> repository)
        {
            _repository = repository;
        }
        public async Task<List<GetDistrictQueryResult>> Handle()
        {
            var values = await _repository.GetAllAsync();
            return values.Select(x => new GetDistrictQueryResult
            {
               DistrictId = x.DistrictId,
               DistrictName = x.DistrictName,
               Temperature = x.Temperature, 
               Date = x.Date,
               CityId = x.CityId,


            }).ToList();
        }



    }
}
