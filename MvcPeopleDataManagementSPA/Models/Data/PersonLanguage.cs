using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcPeopleDataManagementSPA.Models.Data
{
    public class PersonLanguage
    {
        public int PersonId { get; set; }

        public Person Person { get; set; }

        public int LanguageId { get; set; }

        public Language Language { get; set; }
    }
}
