using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcPeopleDataManagementSPA.Database;
using MvcPeopleDataManagementSPA.Models.Data;

namespace MvcPeopleDataManagementSPA.Models.Repo
{
    public class DatabaseCityRepo : ICityRepo
    {
        private readonly PeopleDbContext _personDbContext;

        public DatabaseCityRepo(PeopleDbContext personDbContext)
        {
            _personDbContext = personDbContext;
        }

        public City Create(City city)
        {
            _personDbContext.Cities.Add(city);

            int result = _personDbContext.SaveChanges();

            if (result == 0)
            {
                return null;
            }

            return city;
        }

        public List<City> Read()
        {
            return _personDbContext.Cities.Include(c => c.State)
                                          .Include(c => c.PersonList).ToList();
        }

        public City Read(int id) // return null if not found
        {
            return _personDbContext.Cities.Include(c => c.State)
                                          .Include(c => c.PersonList)
                                          .SingleOrDefault(c => c.Id == id); 
        }

        public City Update(City city)
        {
            City originalCity = Read(city.Id);

            if (originalCity == null)
            {
                return null;
            }

            _personDbContext.Cities.Update(city);

           // originalCity.Name = city.Name;
           // originalCity.State = city.State;

            int result = _personDbContext.SaveChanges();

            if (result == 0) //return zero if there is no change
            {
                return null; 
            }

            return originalCity;
        }

        public bool Delete(City city)
        {
            City originalCity = Read(city.Id);

            if (originalCity == null)
            {
                return false;
            }

            _personDbContext.Remove(originalCity);

            int result = _personDbContext.SaveChanges();

            if (result == 0)
            {
                return false;
            }

            return true;
        }
    }
}
