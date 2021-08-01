using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcPeopleDataManagementSPA.Models.Data;

namespace MvcPeopleDataManagementSPA.Models.ViewModel
{
    public class CityViewModel
    {
        public List<City> CityList { get; set; }

        public City City { get; set; }

        public string CountryName { get; set; }

        public string CityName { get; set; }

        public CityViewModel()
        {
            CityList = new List<City>();
        }
    }
}
