using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcPeopleDataManagementSPA.Database;
using MvcPeopleDataManagementSPA.Models.Data;

namespace MvcPeopleDataManagementSPA.Models.Repo
{
    public class DatabaseCountryRepo : ICountryRepo
    {
        private readonly PeopleDbContext _personDbContext;
      
        public DatabaseCountryRepo(PeopleDbContext personDbContext)
        {
            _personDbContext = personDbContext;
        }

        public Country Create(Country country)
        {
            _personDbContext.Countries.Add(country);

            int result = _personDbContext.SaveChanges();

            if (result == 0)
            {
                return null;
            }

            return country;
        }

        public List<Country> Read()
        {
            return _personDbContext.Countries.Include(c => c.CityList).ToList();
        }

        public Country Read(int id) // return null if not found
        {
            return _personDbContext.Countries.Include(c => c.CityList).SingleOrDefault(c => c.Id == id); 
        }

        public Country Update(Country country)
        {
            Country originalCountry = Read(country.Id);

            if (originalCountry == null)
            {
                return null;
            }

            _personDbContext.Countries.Update(country);

            int result = _personDbContext.SaveChanges();

            if (result == 0)
            {
                return null;
            }

            return originalCountry;
        }

        public bool Delete(Country country)
        {
            Country originalCountry = Read(country.Id);

            if (originalCountry == null)
            {
                return false;
            }

            _personDbContext.Countries.Remove(originalCountry);

            int result = _personDbContext.SaveChanges();

            if (result == 0)
            {
                return false;
            }

            return true;
        }
    }
}
