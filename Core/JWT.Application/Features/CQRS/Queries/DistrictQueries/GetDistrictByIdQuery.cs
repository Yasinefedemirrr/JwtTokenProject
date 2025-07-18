using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWT.Application.Features.CQRS.Queries.DistrictQueries
{
    public class GetDistrictByIdQuery
    {
        public int Id { get; set; }

        public GetDistrictByIdQuery(int id)
        {
            Id = id;
        }
    }
}
