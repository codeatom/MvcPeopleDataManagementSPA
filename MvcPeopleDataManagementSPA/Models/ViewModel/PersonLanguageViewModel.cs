using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcPeopleDataManagementSPA.Models.Data;

namespace MvcPeopleDataManagementSPA.Models.ViewModel
{
    public class PersonLanguageViewModel
    {
        public Person person { get; set; }

        public List<Language> Languages { get; set; }

        public List<Language> UnSpokenLanguages { get; set; }
    }
}
