using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWT.Application.Features.CQRS.Commands.DistrictCommands
{
    public class RemoveDistrictCommand
    {
        public int Id { get; set; }

        public RemoveDistrictCommand(int ıd)
        {
            Id = ıd;
        }
    }
}
