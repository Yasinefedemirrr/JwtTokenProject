﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWT.Domain.Entity
{
    public class District
    {
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public int Temperature { get; set; }
        public DateTime Date { get; set; }

        public int CityId { get; set; }

        [ForeignKey("CityId")]
        public CityWeather CityWeather { get; set; }
    }

}
