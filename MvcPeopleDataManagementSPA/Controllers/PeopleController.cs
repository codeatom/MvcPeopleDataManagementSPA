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
    public class PeopleController : Controller
    {
        private readonly IPeopleService _peopleService;
        private readonly ICountryService _countryService;
        private readonly ILanguageService _languageService;
        private readonly IPersonLanguageService _personLanguageService;

        public PeopleController
            (
            IPeopleService peopleService,
            ICountryService countryService,
            ICityService cityService,
            ILanguageService languageService,
            IPersonLanguageService personLanguageService
            )
        {
            _peopleService = peopleService;
            _countryService = countryService;
            _languageService = languageService;
            _personLanguageService = personLanguageService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["ShowActionLinks"] = true;

            PeopleViewModel peopleViewModel = new PeopleViewModel();

            peopleViewModel.PersonViewModel = _peopleService.All();   //Get all persons in the database
            peopleViewModel.CountryViewModel = _countryService.All(); //Get all existing countries

            return View(peopleViewModel);
        }

        [HttpPost]
        public IActionResult Index(PeopleViewModel peopleViewModel)
        {
            ViewData["ShowActionLinks"] = true; //Allow action links

            string searchString = peopleViewModel.PersonViewModel.FilterText;           //To search for a person
            List<Person> filteredPersons = _peopleService.FindByKeyWord(searchString);  //To search for a person

            peopleViewModel.PersonViewModel.PersonList = filteredPersons;
            peopleViewModel.CountryViewModel = _countryService.All();

            ModelState.Clear();
            return View(peopleViewModel);
        }

        [HttpPost]
        public IActionResult Create(CreatePerson CreatePerson)
        {
            ViewData["ShowActionLinks"] = true; //Allow action links

            PeopleViewModel peopleViewModel = new PeopleViewModel();

            if (ModelState.IsValid)
            {
                if (CreatePerson.CityId > 0 && CreatePerson.CountryId > 0)
                {
                    _peopleService.Add(CreatePerson);
                    return RedirectToAction(nameof(Index));
                }
            }
          
            peopleViewModel.PersonViewModel = _peopleService.All();
            peopleViewModel.CountryViewModel = _countryService.All();
            peopleViewModel.CreatePerson = CreatePerson;
          
            return View("Index", peopleViewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Person person = _peopleService.FindById(id);
            EditPerson editPerson = new EditPerson();

            if (person == null)
            {
                return RedirectToAction("Index");
            }
           
            editPerson.Id = id;
            editPerson.StateId = person.State.Id;
            editPerson.CityId = person.CurrentCity.Id;
            editPerson.CreatePerson = _peopleService.PersonToCreatePerson(person);
            editPerson.CityList = _countryService.FindById(person.State.Id).CityList;
         
            return View(editPerson);
        }

        [HttpPost]
        public IActionResult Edit(int id, CreatePerson createPerson)
        {
            EditPerson editPerson = new EditPerson();
            Person person = new Person();

            if (ModelState.IsValid)
            {
                person = _peopleService.Edit(id, createPerson);
                return RedirectToAction(nameof(Index));
            }

            person = _peopleService.FindById(id); //find the person instead

            editPerson.Id = id;
            editPerson.StateId = person.State.Id;
            editPerson.CityId = person.CurrentCity.Id;
            editPerson.CreatePerson = _peopleService.PersonToCreatePerson(person);
            editPerson.CityList = _countryService.FindById(person.State.Id).CityList;

            return View(editPerson);
        }

        public IActionResult Details(int id)
        {
            Person person = _peopleService.FindById(id);

            if (person == null)
            {
                return RedirectToAction("Index");
            }

            return View(person);
        }

        public IActionResult ManagePersonLanguages(int id)
        {
            Person person = _peopleService.FindById(id);
            PersonLanguageViewModel personLanguageViewModel = new PersonLanguageViewModel();

            if (person == null)
            {
                return RedirectToAction("Index");
            }

            personLanguageViewModel.person = person;
            personLanguageViewModel.Languages = _languageService.All();
            personLanguageViewModel.UnSpokenLanguages = _peopleService.LanguageState(id);

            return View(personLanguageViewModel);
        }

        [HttpGet]
        public IActionResult AddPersonLanguage(int personId, int languageId)
        {
            Person person = _peopleService.FindById(personId);

            if (person == null)
            {
                return RedirectToAction("Index");
            }

            _personLanguageService.Add(new PersonLanguage() { PersonId = personId, LanguageId = languageId });

            return RedirectToAction("ManagePersonLanguages", new { id = personId });
        }

        [HttpGet]
        public IActionResult RemovePersonLanguage(int personId, int languageId)
        {
            Person person = _peopleService.FindById(personId);

            if (person == null)
            {
                return RedirectToAction("Index");
            }
            
            _personLanguageService.Remove(personId, languageId);

            return RedirectToAction("ManagePersonLanguages", new { id = personId });
        }

        public ActionResult DeleteRequest(int id)
        {
            PersonViewModel personViewModel = new PersonViewModel();
            personViewModel.Person = _peopleService.FindById(id);

            return View(personViewModel);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            Person person = _peopleService.FindById(id);

            if (person != null)
            {
                _peopleService.Remove(id);
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}