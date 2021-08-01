using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcPeopleDataManagementSPA.Models.Data;

namespace MvcPeopleDataManagementSPA.Models.ViewModel
{
    public class EditPerson
    {
        public int Id { get; set; }

        public CreatePerson CreatePerson { get; set; }

        public List<Country> CountryList { get; set; }

        public List<City> CityList { get; set; }

        public int City { get; set; }

        public int CityId { get; set; }

        public int StateId { get; set; }

        public EditPerson()
        {
            CityList = new List<City>();
            CountryList = new List<Country>();
        }
    }
}
