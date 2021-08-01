using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcPeopleDataManagementSPA.Models.Data;
using MvcPeopleDataManagementSPA.Models.Repo;
using MvcPeopleDataManagementSPA.Models.ViewModel;

namespace MvcPeopleDataManagementSPA.Models.Service
{
    public class LanguageService : ILanguageService
    {
        private readonly ILanguageRepo _languageRepo;
        private readonly IPeopleRepo _peopleRepo;

        public LanguageService(ILanguageRepo languageRepo, IPeopleRepo peopleRepo)
        {
            _languageRepo = languageRepo;
            _peopleRepo = peopleRepo;
        }

        public Language Add(CreateLanguage createLanguage)
        {
            Language language = new Language() { Name = createLanguage.LanguageName };
            return _languageRepo.Create(language);
        }

        public List<Language> All()
        {
            return _languageRepo.Read();
        }

        public Language FindById(int id)
        {
            return _languageRepo.Read(id);
        }

        public Language Edit(int id, CreateLanguage createLanguage)
        {
            Language language = FindById(id);

            if (language == null)
            {
                return null;
            }

            language.Name = createLanguage.LanguageName;

            language = _languageRepo.Update(language);

            return language;
        }

        public CreateLanguage LanguageToCreateLanguage(Language language)
        {
            CreateLanguage createLanguage = new CreateLanguage();

            createLanguage.LanguageName = language.Name;
            
            return createLanguage;
        }

        public bool LanguageIsConnected(int id)
        {
            Language language = _languageRepo.Read(id);
            List<Person> people = _peopleRepo.Read();

            foreach(Person person in people)
            {
                foreach(PersonLanguage personLanguage in person.PersonLanguages)
                {
                    if (personLanguage.Language.Id == id)
                    {
                        return true;
                    }
                        
                }
            }

            return false;
        }

        public bool Remove(int id)
        {
            Language language = _languageRepo.Read(id);
            if (language != null)
            {
                return _languageRepo.Delete(language);
            }

            return false;
        }
    }
}
