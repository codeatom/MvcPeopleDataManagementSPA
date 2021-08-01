using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcPeopleDataManagementSPA.Database;
using MvcPeopleDataManagementSPA.Models.Data;

namespace MvcPeopleDataManagementSPA.Models.Repo
{
    public class DatabasePersonLanguageRepo : IPersonLanguageRepo
    {
        private readonly PeopleDbContext _peopleDbContext;

        public DatabasePersonLanguageRepo(PeopleDbContext peopleDbContext)
        {
            _peopleDbContext = peopleDbContext;
        }

        public PersonLanguage Create(PersonLanguage personLanguage)
        {
            _peopleDbContext.PersonLanguages.Add(personLanguage);

            if (_peopleDbContext.SaveChanges() > 0)
            {
                return personLanguage;
            }

            return null;
        }

        public PersonLanguage Read(int personId, int languageId)
        {
            return _peopleDbContext.PersonLanguages.SingleOrDefault(pl => pl.PersonId == personId && pl.LanguageId == languageId);
        }

        public List<PersonLanguage> Read()
        {
            return _peopleDbContext.PersonLanguages.ToList();
        }

        public bool Delete(int personId, int languageId)
        {
            PersonLanguage personLanguage = Read(personId, languageId);           

            if (personLanguage == null)
            {
                return false;
            }

            _peopleDbContext.PersonLanguages.Remove(personLanguage);

            if(_peopleDbContext.SaveChanges() > 0)
            {
                return true;
            }

            return false;
        }
    }
}
