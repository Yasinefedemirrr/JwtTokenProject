using System;
using System.ComponentModel.DataAnnotations; 

namespace JWT.Domain.Entity
{
    public class CityWeather
    {
        [Key] 
        public int CityId { get; set; }

        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int Temperature { get; set; }
    }
}
