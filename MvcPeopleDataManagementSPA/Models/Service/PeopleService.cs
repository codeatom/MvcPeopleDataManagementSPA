using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MvcPeopleDataManagementSPA.Models.Data;
using MvcPeopleDataManagementSPA.Models.Repo;
using MvcPeopleDataManagementSPA.Models.ViewModel;

namespace MvcPeopleDataManagementSPA.Models.Service
{
    public class PeopleService : IPeopleService
    {  
        private readonly ICityRepo _cityRepo;
        private readonly IPeopleRepo _peopleRepo;
        private readonly ILanguageRepo _languageRepo;
        private readonly IPersonLanguageRepo _personLanguageRepo;

        public PeopleService
            (
            IPeopleRepo peopleRepo, 
            ICityRepo cityRepo, 
            ILanguageRepo languageRepo, 
            IPersonLanguageRepo personLanguageRepo
            )
        {
            _cityRepo = cityRepo;
            _peopleRepo = peopleRepo;
            _languageRepo = languageRepo;
            _personLanguageRepo = personLanguageRepo;
        }

        public Person Add(CreatePerson createPerson)
        {
            Person person = new Person();

            person.FirstName = createPerson.FirstName;
            person.LastName = createPerson.LastName;
            person.CurrentCity = _cityRepo.Read(createPerson.CityId);
            person.State = _cityRepo.Read(createPerson.CityId).State;
            person.CellphoneNumber = createPerson.CellphoneNumber;

            person = _peopleRepo.Create(person);

            return person;
        }

        public PersonViewModel All()
        {
            PersonViewModel personViewModel = new PersonViewModel();
            personViewModel.PersonList = _peopleRepo.Read();

            return personViewModel;
        }

        public PersonViewModel All_2()  //Added for json data manipulation
        {
            PersonViewModel personViewModel = new PersonViewModel();
            personViewModel.PersonList = _peopleRepo.Read_2();

            return personViewModel;
        }


        public Person FindById(int id)
        {
            return _peopleRepo.Read(id);
        }

        public Person FindById_2(int id)  //Added for json data manipulation
        {
            return _peopleRepo.Read_2(id);
        }

        public List<Person> FindByKeyWord(string keyWord)
        {
            List<Person> personList = new List<Person>();

            string searchStr = "";
            string currentCity = "";
            string firstName = "";
            string lastName = "";
            string fullName = "";

            if (string.IsNullOrWhiteSpace(keyWord))
            {
                return personList;
            }
            else
            {
                searchStr = keyWord.ToLower().Replace(" ", "");
            }

            foreach (Person person in _peopleRepo.Read())
            {
                currentCity = person.CurrentCity.Name.ToLower();
                firstName = person.FirstName.ToLower();
                lastName = person.LastName.ToLower();

                fullName = firstName + lastName;

                if (fullName.Equals(searchStr) ||
                    currentCity.Equals(searchStr) ||
                    firstName.Equals(searchStr) ||
                    lastName.Equals(searchStr))
                {
                    personList.Add(person);
                }
            }

            return personList;
        }

        public Person Edit(int id, CreatePerson createPerson)
        {
            Person originalPerson = FindById(id);

            if (originalPerson == null)
            {
                return null;
            }

            originalPerson.FirstName = createPerson.FirstName;
            originalPerson.LastName = createPerson.LastName;
            originalPerson.CurrentCity = _cityRepo.Read(createPerson.CityId);
            originalPerson.State = _cityRepo.Read(createPerson.CityId).State;
            originalPerson.CellphoneNumber = createPerson.CellphoneNumber;

            originalPerson = _peopleRepo.Update(originalPerson);

            return originalPerson;
        }

        public CreatePerson PersonToCreatePerson(Person person)
        {
            CreatePerson createPerson = new CreatePerson();

            createPerson.FirstName = person.FirstName;
            createPerson.LastName = person.LastName;
            createPerson.CityId = person.CurrentCity.Id;

            createPerson.CellphoneNumber = person.CellphoneNumber;

            return createPerson;
        }

        public List<Language> LanguageState(int id)
        {
            List<Language> availableLanguages = _languageRepo.Read();
            List<Language> unSpokenLanguages = new List<Language>();
            

            Person person = FindById(id);

            foreach (var language in availableLanguages)
            {
                if (_personLanguageRepo.Read(id, language.Id) == null)
                {
                    unSpokenLanguages.Add(language);
                }
            }

            return unSpokenLanguages;
        }

        public bool Remove(int id)
        {
            return _peopleRepo.Delete(id);
        }
    }
}