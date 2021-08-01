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
    [Authorize]
    public class AjaxPeopleController : Controller
    {
        IPeopleService _peopleService;
        private readonly ICountryService _countryService;

        public AjaxPeopleController(IPeopleService peopleService, ICountryService countryService)
        {
            _peopleService = peopleService;
            _countryService = countryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DisplayPerson(int id)
        {
            ViewData["ShowActionLinks"] = false; // Prevents action links in Ajax-people-List

            Person person = _peopleService.FindById(id);

            if (person != null)
            {
                return PartialView("_PersonPartialView", person);
            }

            return Ok("There is no person with id " + id);
        }

        [HttpPost]
        public IActionResult DisplayPeople()
        {
            ViewData["ShowActionLinks"] = false; // Prevents action links in Ajax-people-List

            List<Person> persons = _peopleService.All().PersonList;

            if (persons.Count != 0)
            {
                return PartialView("_PeoplePartialView", persons);
            }

            return Ok("There is no person in the list");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult RemovePerson(int id)
        {
            Person person = _peopleService.FindById(id);

            if (person != null)
            {
                _peopleService.Remove(id);
                return Ok("Person with id " + id + " removed.");
            }

            return Ok("There is no person with id " + id);
        }

        [HttpGet]
        public IActionResult GetcountryCities(int id)
        {
            PeopleViewModel peopleViewModel = new PeopleViewModel();
            
            Country country = _countryService.FindById(id);

            if (country != null)
            {
                peopleViewModel.CityList = country.CityList;
                return PartialView("_CitiesPartialView", peopleViewModel);
            }

            return Ok();
        }

    }
}