using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MvcPeopleDataManagementSPA.Models.Data;

namespace MvcPeopleDataManagementSPA.Models.ViewModel
{
    public class CreateCountry
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "Country")]
        public string Name { get; set; }
    }
}
