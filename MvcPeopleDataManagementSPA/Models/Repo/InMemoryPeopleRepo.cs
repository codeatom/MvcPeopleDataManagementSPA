using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcPeopleDataManagementSPA.Models.Data;

namespace MvcPeopleDataManagementSPA.Models.Repo
{
    public class InMemoryPeopleRepo : IPeopleRepo
    {
        List<Person> personList = new List<Person>();
        int idCounter = 0;

        public Person Create(Person person)
        {
            person.Id = ++idCounter;
            personList.Add(person);

            return person;
        }

        public Person Read(int id)
        {
            foreach (Person item in personList)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }

            return null;
        }

        public List<Person> Read()
        {
            return personList;
        }

        public Person Update(Person person)
        {
            Person originalPerson = Read(person.Id);

            if (originalPerson == null)
            {
                return null;
            }

            originalPerson.FirstName = person.FirstName;
            originalPerson.LastName = person.LastName;
            originalPerson.CurrentCity = person.CurrentCity;
            originalPerson.CellphoneNumber = person.CellphoneNumber;

            return originalPerson;
        }

        public bool Delete(int id)
        {
            Person person = Read(id);

            if (person == null)
            {
                return false;
            }

            return personList.Remove(person);
        }

        public List<Person> Read_2()
        {
            throw new NotImplementedException();
        }

        public Person Read_2(int id)
        {
            throw new NotImplementedException();
        }
    }
}