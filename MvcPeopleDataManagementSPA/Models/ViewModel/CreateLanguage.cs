using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcPeopleDataManagementSPA.Models.ViewModel
{
    public class CreateLanguage
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LanguageName { get; set; }
    }
}
