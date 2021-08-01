using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcPeopleDataManagementSPA.Models.Data;
using MvcPeopleDataManagementSPA.Models.Repo;
using MvcPeopleDataManagementSPA.Models.ViewModel;

namespace MvcPeopleDataManagementSPA.Models.Service
{
    public class CityService : ICityService
    {
        private readonly ICityRepo _cityRepo;
        private readonly ICountryRepo _countryRepo;

        public CityService(ICityRepo cityRepo, ICountryRepo countryRepo)
        {
            _cityRepo = cityRepo;
            _countryRepo = countryRepo;
        }

        public City Add(CreateCity createCity)
        {
            City city = new City();
            Country country = _countryRepo.Read(createCity.StateId);

            if(country == null)
            {
                return null;
            }

            city.Name = createCity.CityName;
            city.State = country;
            city = _cityRepo.Create(city);
            city.StateId = country.Id;

            country.CityList.Add(city);        

            return city;
        }

        public CityViewModel All()
        {
            CityViewModel cityViewModel = new CityViewModel();
            cityViewModel.CityList = _cityRepo.Read();

            return cityViewModel;
        }

        public CityViewModel All_2()  //Added for json data manipulation
        {
            CityViewModel cityViewModel = new CityViewModel();
            cityViewModel.CityList = _cityRepo.Read();

            foreach (City city in cityViewModel.CityList)
            {
                city.PersonList = null;
                city.State = null;
            }

            return cityViewModel;
        }

        public City FindById(int id)
        {
            return _cityRepo.Read(id);
        }

        public City Edit(int id, CreateCity createCity)
        {
            City originalCity = FindById(id);

            if (originalCity == null)
            {
                return null;
            }

            originalCity.Name = createCity.CityName;

            originalCity = _cityRepo.Update(originalCity);

            return originalCity;
        }

        public CreateCity CityToCreateCity(City city)
        {
            CreateCity createCity = new CreateCity();

            createCity.CityName = city.Name;
            createCity.CountryName = city.Name;

            return createCity;
        }

        public bool Remove(int id)
        {
            City city = _cityRepo.Read(id);

            if (city != null)
            {
                return _cityRepo.Delete(city);
            }

            return false;
        }

    }
}
