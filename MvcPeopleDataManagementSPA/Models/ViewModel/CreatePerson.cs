using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using MvcPeopleDataManagementSPA.Models.Data;

namespace MvcPeopleDataManagementSPA.Models.ViewModel
{
    public class CreatePerson
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string LastName { get; set; }

        [Required]
        public int CountryId { get; set; }

        [Required]
        public int CityId { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string CellphoneNumber { get; set; }

        public List<Country> CountryList { get; set; }

        public List<City> CurrentCityList { get; set; }

        public CreatePerson()
        {
            CountryList = new List<Country>();
            CurrentCityList = new List<City>();
        }
    }
}
