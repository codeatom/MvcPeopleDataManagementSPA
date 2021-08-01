using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcPeopleDataManagementSPA.Models.Data;
using MvcPeopleDataManagementSPA.Models.Service;
using MvcPeopleDataManagementSPA.Models.ViewModel;

namespace MvcPeopleDataManagementSPA.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CityController : Controller
    {
        private readonly ICountryService _countryService;
        private readonly ICityService _cityService;
      

        public CityController(ICountryService countryService, ICityService cityService)
        {
            _cityService = cityService;
            _countryService = countryService;
        }

        public IActionResult Index()
        {
            CountryViewModel countryViewModel = new CountryViewModel();
            countryViewModel = _countryService.All();

            return View(countryViewModel);
        }

        public ActionResult Create()
        {
            CreateCity createCity = new CreateCity();
            createCity.CountryList = _countryService.Countries();
            return View(createCity);
        }

        [HttpPost]
        public ActionResult Create(CreateCity createCity)
        {      
            if (ModelState.IsValid)
            {
                _cityService.Add(createCity);
                return RedirectToAction(nameof(Index));
            }

            createCity.CountryList = _countryService.Countries();

            return View(createCity);
        }

        public ActionResult AddCity(int id)
        {
            CreateCity createCity = new CreateCity();

            Country country = _countryService.FindById(id);

            createCity.CountryName = country.Name;
            createCity.StateId = country.Id;

            return View(createCity);
        }

        [HttpPost]
        public ActionResult AddCity(CreateCity createCity)
        {
            if (ModelState.IsValid)
            {
                _cityService.Add(createCity);
                return RedirectToAction(nameof(Index));
            }

            return View("AddCity", createCity.StateId);
        }

        public IActionResult DisplayCities(int id)
        {
            Country country = _countryService.FindById(id);
            CityViewModel cityViewModel = new CityViewModel();

            if (country != null)
            {
                cityViewModel.CityList = country.CityList;
                cityViewModel.CountryName = country.Name;
            }

            return View(cityViewModel);
        }

        public ActionResult Edit(int id)
        {
            City city = _cityService.FindById(id);
            EditCity editCity = new EditCity();

            if (city == null)
            {
                return RedirectToAction("Index");
            }

            editCity.Id = id;
            editCity.CreateCity = _cityService.CityToCreateCity(city);
            
            return View(editCity);
        }

        [HttpPost]
        public IActionResult Edit(int id, CreateCity createCity)
        {
            EditCity editCity = new EditCity();

            if (ModelState.IsValid)
            {
                _cityService.Edit(id, createCity);
                return RedirectToAction(nameof(Index));
            }

            editCity.Id = id;
            editCity.CreateCity = createCity;

            return View(editCity);
        }

        [HttpGet]
        public ActionResult DeleteRequest(int id)
        {
            CityViewModel cityViewModel = new CityViewModel();
            cityViewModel.City = _cityService.FindById(id);

            return View(cityViewModel);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            City city = _cityService.FindById(id);

            if (city.PersonList.Count == 0)
            {
                _cityService.Remove(id);
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
