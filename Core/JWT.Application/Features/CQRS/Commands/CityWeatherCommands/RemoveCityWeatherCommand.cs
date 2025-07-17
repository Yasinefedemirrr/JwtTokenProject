using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWT.Application.Features.CQRS.Commands.CityWeatherCommands
{
    public class RemoveCityWeatherCommand
    {
        public int Id { get; set; }

        public RemoveCityWeatherCommand(int ıd)
        {
            Id = ıd;
        }
    }
}
