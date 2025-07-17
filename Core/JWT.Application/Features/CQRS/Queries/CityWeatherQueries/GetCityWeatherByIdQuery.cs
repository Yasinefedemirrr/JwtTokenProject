using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWT.Application.Features.CQRS.Queries.CityWeatherQueries
{
    public class GetCityWeatherByIdQuery
    {

        public int Id { get; set; }

        public GetCityWeatherByIdQuery(int id)
        {
            Id = id;
        }
    }
}
