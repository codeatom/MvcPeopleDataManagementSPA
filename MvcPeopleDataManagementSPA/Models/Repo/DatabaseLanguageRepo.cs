using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcPeopleDataManagementSPA.Database;
using MvcPeopleDataManagementSPA.Models.Data;

namespace MvcPeopleDataManagementSPA.Models.Repo
{
    public class DatabaseLanguageRepo : ILanguageRepo
    {
        private readonly PeopleDbContext _personDbContext;

        public DatabaseLanguageRepo(PeopleDbContext personDbContext)
        {
            _personDbContext = personDbContext;
        }

        public Language Create(Language language)
        {
            _personDbContext.Add(language);

            if(_personDbContext.SaveChanges() > 0)
            {
                return language;
            }

            return null;
        }

        public Language Read(int id)
        {
            return _personDbContext.Languages.SingleOrDefault(L => L.Id == id);
        }

        public List<Language> Read()
        {
            return _personDbContext.Languages.ToList();
        }

        public Language Update(Language language)
        {
            Language originalLanguage = Read(language.Id);

            if (originalLanguage == null)
            {
                return null;
            }

            _personDbContext.Languages.Update(language);

            int result = _personDbContext.SaveChanges();

            if (result == 0) //return zero if there is no change
            {
                return null;
            }

            return originalLanguage;
        }

        public bool Delete(Language language)
        {
            Language originalLanguage = Read(language.Id);

            if (originalLanguage == null)
            {
                return false;
            }

            _personDbContext.Remove(originalLanguage);

            int result = _personDbContext.SaveChanges();

            if (result == 0)
            {
                return false;
            }

            return true;
        }
    }
}
