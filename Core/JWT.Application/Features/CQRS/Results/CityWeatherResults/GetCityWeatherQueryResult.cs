using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWT.Application.Features.CQRS.Results.CityWeatherResults
{
    public class GetCityWeatherQueryResult
    {
        public int CityId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int Temperature { get; set; }
    }
}
