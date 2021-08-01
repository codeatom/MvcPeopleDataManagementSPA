using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MvcPeopleDataManagementSPA.Models.Data;

namespace MvcPeopleDataManagementSPA.Models.ViewModel
{
    public class CreateCity
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string CityName { get; set; }

        public string CountryName { get; set; }

        public int StateId { get; set; }

        public List<Country> CountryList { get; set; }
    }
}
