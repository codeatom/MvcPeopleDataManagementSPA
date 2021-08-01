using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcPeopleDataManagementSPA.Models.Data;
using MvcPeopleDataManagementSPA.Models.Repo;

namespace MvcPeopleDataManagementSPA.Models.Service
{
    public class PersonLanguageService : IPersonLanguageService
    {
        private readonly IPersonLanguageRepo _personLanguageRepo;

        public PersonLanguageService(IPersonLanguageRepo personLanguageRepo)
        {
            _personLanguageRepo = personLanguageRepo;
        }


        public PersonLanguage Add(PersonLanguage personLanguage)
        {
            return _personLanguageRepo.Create(personLanguage);
        }

        public List<PersonLanguage> All()
        {
            return _personLanguageRepo.Read();
        }

        public PersonLanguage FindById(int personId, int languageId)
        {
            return _personLanguageRepo.Read(personId, languageId);
        }

        public bool Remove(int personId, int languageId)
        {
            return _personLanguageRepo.Delete(personId, languageId);
        }
    }
}
