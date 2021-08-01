using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcPeopleDataManagementSPA.Models.Data;

namespace MvcPeopleDataManagementSPA.Models.Service
{
    public interface IPersonLanguageService
    {
        public PersonLanguage Add(PersonLanguage personLanguage);

        public List<PersonLanguage> All();

        public PersonLanguage FindById(int personId, int languageId);

        public bool Remove(int personId, int languageId);
    }
}
