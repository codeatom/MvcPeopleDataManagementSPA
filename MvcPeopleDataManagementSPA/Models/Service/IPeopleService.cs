using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcPeopleDataManagementSPA.Models.Data;
using MvcPeopleDataManagementSPA.Models.ViewModel;

namespace MvcPeopleDataManagementSPA.Models.Service
{
    public interface IPeopleService
    {
        public Person Add(CreatePerson createPerson);

        public PersonViewModel All();

        public PersonViewModel All_2(); //Added for json data manipulation

        public Person FindById(int id);

        public Person FindById_2(int id); //Added for json data manipulation

        public List<Person> FindByKeyWord(string names);

        public Person Edit(int id, CreatePerson createPerson);

        public CreatePerson PersonToCreatePerson(Person person);

        List<Language> LanguageState(int id);

        public bool Remove(int id);
        
    }
}
