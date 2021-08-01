using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MvcPeopleDataManagementSPA.Models.Data;

namespace MvcPeopleDataManagementSPA.Models.ViewModel
{
    public class PeopleViewModel
    {
        public PersonViewModel PersonViewModel { get; set; }

        public CreatePerson CreatePerson { get; set; }

        public CountryViewModel CountryViewModel { get; set; }

        public CityViewModel CityViewModel { get; set; }

        public List<Country> CountryList { get; set; }

        public List<City> CityList { get; set; }

        public int StateId { get; set; }

        public PeopleViewModel()
        {
            PersonViewModel = new PersonViewModel();
            CreatePerson = new CreatePerson();
            CityList = new List<City>();
            CountryList = new List<Country>();
            CountryViewModel = new CountryViewModel();
        }
    }
}
