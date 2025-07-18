using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWT.Application.Features.CQRS.Commands.DistrictCommands
{
    public class CreateDistrictCommand
    {
        public string DistrictName { get; set; }
        public int Temperature { get; set; }
        public DateTime Date { get; set; }
        public int CityId { get; set; }
    }
}
