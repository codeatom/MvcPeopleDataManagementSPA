using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcPeopleDataManagementSPA.Models.Data;
using MvcPeopleDataManagementSPA.Models.ViewModel;

namespace MvcPeopleDataManagementSPA.Models.Service
{
    public interface ILanguageService
    {
        public Language Add(CreateLanguage createLanguage);

        public List<Language> All();

        public Language FindById(int id);

        public CreateLanguage LanguageToCreateLanguage(Language language);

        public bool Remove(int id);

        public bool LanguageIsConnected(int id);

        public Language Edit(int id, CreateLanguage createLanguage);

        //public LanguageViewModel All();


    }
}
