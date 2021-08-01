using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcPeopleDataManagementSPA.Models.Data
{
    public class Country
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
       
        public List<City> CityList { get; set; }

        public Country()
        {
            CityList = new List<City>();
        }
    }
}