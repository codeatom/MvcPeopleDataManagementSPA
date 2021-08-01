using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcPeopleDataManagementSPA.Models.Data;
using MvcPeopleDataManagementSPA.Models.ViewModel;

namespace MvcPeopleDataManagementSPA.Models.Service
{
    public interface ICountryService
    {
        public Country Add(CreateCountry createCountry);

        public CountryViewModel All();

        public CountryViewModel All_2(); //Added for json data manipulation

        public List<Country> Countries();

        public Country FindById(int id);

        public Country Edit(int id, CreateCountry createCountry);

        public CreateCountry CountryToCreateCountry(Country country);

        public bool Remove(int id);
    }
}
