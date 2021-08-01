using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcPeopleDataManagementSPA.Database;
using MvcPeopleDataManagementSPA.Models.Data;

namespace MvcPeopleDataManagementSPA.Models.Repo
{
    public class DatabasePeopleRepo : IPeopleRepo
    {
        private readonly PeopleDbContext _personDbContext;

        public DatabasePeopleRepo(PeopleDbContext personDbContext)
        {
            _personDbContext = personDbContext;
        }

        public Person Create(Person person)
        {
            _personDbContext.Persons.Add(person);

            int result = _personDbContext.SaveChanges();

            if (result == 0)
            {
                throw new Exception("Unable to create person in database");
            }

            return person;
        }

        public Person Read(int id)   // return null if not found
        {
            return _personDbContext.Persons.Include(p => p.CurrentCity)
                                           .Include(p => p.State)
                                           .Include(p => p.PersonLanguages)
                                               .ThenInclude(pl => pl.Language)
                                           .SingleOrDefault(p => p.Id == id); 
        }

        public Person Read_2(int id)  //Added for json data manipulation
        {
            Person person = _personDbContext.Persons.Include(p => p.CurrentCity)
                                           .Include(p => p.State)
                                           .Include(p => p.PersonLanguages)
                                               .ThenInclude(pl => pl.Language)
                                           .SingleOrDefault(p => p.Id == id);

            person.CurrentCity.PersonList = null;
            person.State.CityList = null;
            person.PersonLanguages = null;

            return person;
        }

        public List<Person> Read()
        {
            return _personDbContext.Persons.Include(p => p.CurrentCity)
                                           .Include(p => p.State)
                                           .Include(p => p.PersonLanguages)
                                               .ThenInclude(pl => pl.Language)
                                           .ToList();
        }

        public List<Person> Read_2()  //Added for json data manipulation
        {
            List<Person> persons = _personDbContext.Persons.Include(p => p.CurrentCity)
                                           .Include(p => p.State)
                                           .Include(p => p.PersonLanguages)
                                               .ThenInclude(pl => pl.Language)
                                           .ToList();

            foreach (Person person in persons)
            {
                person.CurrentCity.PersonList = null;
                person.State.CityList = null;
                person.PersonLanguages = null;
            }

            return persons;
        }

        public Person Update(Person person)
        {
            Person originalPerson = Read(person.Id);
           
            if (originalPerson == null)
            {
                return null;
            }

            _personDbContext.Persons.Update(person);

            //originalPerson.FirstName = person.FirstName;
            //originalPerson.LastName = person.LastName;
            //originalPerson.CurrentCity = person.CurrentCity;
            //originalPerson.CellphoneNumber = person.CellphoneNumber;

            int result = _personDbContext.SaveChanges();

            if (result == 0)
            {
                return null;
            }

            return originalPerson;
        }

        public bool Delete(int id)
        {
            Person originalPerson = Read(id);

            if (originalPerson == null)
            {
                return false;
            }

            _personDbContext.Persons.Remove(originalPerson);

            int result = _personDbContext.SaveChanges();

            if (result == 0)
            {
                return false;
            }

            return true;
        }
    }
}
