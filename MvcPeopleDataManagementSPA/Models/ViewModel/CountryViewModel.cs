using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcPeopleDataManagementSPA.Models.Data;

namespace MvcPeopleDataManagementSPA.Models.ViewModel
{
    public class CountryViewModel
    {
        public List<Country> CountryList { get; set; }

        public Country Country { get; set; }

        public string CountryName { get; set; }

        public City City { get; set; }

    }
}
