using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class CountryController : Controller
    {
        private readonly ICountryService _countryService;
        private readonly ICityService _cityService;

        public CountryController(ICountryService countryService, ICityService cityService)
        {
            _countryService = countryService;
            _cityService = cityService;
        }

        public IActionResult Index()
        {
            CountryViewModel countryViewModel = new CountryViewModel();
            countryViewModel = _countryService.All();

            return View(countryViewModel);
        }

        public ActionResult Create()
        {
            CreateCountry createCountry = new CreateCountry();
            return View(createCountry);
        }

        [HttpPost]
        public ActionResult Create(CreateCountry createCountry)
        {
            if (ModelState.IsValid)
            {
                _countryService.Add(createCountry);
                return RedirectToAction(nameof(Index));
            }
           
             return View(createCountry);
        }

        public ActionResult Edit(int id)
        {
            Country country = _countryService.FindById(id);
            EditCountry editCountry = new EditCountry();

            if (country == null)
            {
                return RedirectToAction("Index");
            }

            editCountry.Id = id;
            editCountry.CreateCountry = _countryService.CountryToCreateCountry(country);
 
            return View(editCountry);
        }

        [HttpPost]
        public IActionResult Edit(int id, CreateCountry createCountry)
        {
            EditCountry editCountry = new EditCountry();

            if (ModelState.IsValid)
            {
                _countryService.Edit(id, createCountry);
                return RedirectToAction(nameof(Index));
            }
 
            editCountry.Id = id;
            editCountry.CreateCountry = createCountry;                   

            return View(editCountry);
        }

        [HttpGet]
        public ActionResult DeleteRequest(int id)
        {
            CountryViewModel countryViewModel = new CountryViewModel();
            countryViewModel.Country = _countryService.FindById(id);

            return View(countryViewModel);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Country country = _countryService.FindById(id);

            if(country.CityList.Count == 0)
            {
                _countryService.Remove(id);
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
