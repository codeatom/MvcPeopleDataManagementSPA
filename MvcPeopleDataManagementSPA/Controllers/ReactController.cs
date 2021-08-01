using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using MvcPeopleDataManagementSPA.Models.Service;
using MvcPeopleDataManagementSPA.Models.Data;
using MvcPeopleDataManagementSPA.Models.ViewModel;

namespace WebAppPeople.Controllers
{
    [EnableCors("ReactPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReactController : ControllerBase
    {
        private readonly IPeopleService _peopleService;
        private readonly ICountryService _countryService;
        private readonly ICityService _cityService;

        public ReactController(IPeopleService peopleService, ICountryService countryService, ICityService cityService)
        {
            _peopleService = peopleService;
            _countryService = countryService;
            _cityService = cityService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Person>> Get()
        {
            return Ok(_peopleService.All_2().PersonList);
        }

        [HttpGet("{id}")]
        public ActionResult<Person> Get(int id)
        {
            Person person = _peopleService.FindById_2(id);

            if (person == null)
            {
                return BadRequest();
            }

            return Ok(person);
        }

        [HttpPost("/api/Person")]
        public ActionResult<Person> Post([FromBody] CreatePerson newPerson)
        {
            if (ModelState.IsValid)
            {
                Person person = _peopleService.Add(newPerson);

                if (person == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }

                person.PersonLanguages = null;

                return Created("", person);
            }

            return BadRequest(newPerson);
        }

        [HttpDelete("{id}")]
        public void DeletePerson(int id)
        {
            if (!_peopleService.Remove(id))
            {
                Response.StatusCode = 200;
            }

            Response.StatusCode = 400;
        }

        [HttpGet("/api/Country")]
        public ActionResult<IEnumerable<Country>> GetCountries()
        {
            List<Country> countries = _countryService.All_2().CountryList;
            return Ok(countries);
        }

        [HttpGet("/api/City")]
        public ActionResult<IEnumerable<City>> GetCities()
        {
            List<City> cities = _cityService.All_2().CityList;
            return Ok(cities);
        }


        [HttpPost("/api/Country")]
        public ActionResult<Country> Post([FromBody] CreateCountry newCountry)
        {
            if (ModelState.IsValid)
            {
                Country country = _countryService.Add(newCountry);

                if (country == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }

                country.CityList = null;

                return Created("", country);
            }

            return BadRequest(newCountry);
        }


        [HttpPost("/api/City")]
        public ActionResult<City> Post([FromBody] CreateCity newCity)
        {
            if (ModelState.IsValid)
            {
                City city = _cityService.Add(newCity);

                if (city == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }

                city.PersonList = null;

                return Created("", city);
            }

            return BadRequest(newCity);
        }

        [HttpDelete("/api/City/{id}")]
        public void DeleteCity(int id)
        {
            if (!_cityService.Remove(id))
            {
                Response.StatusCode = 200;
            }

            Response.StatusCode = 400;
        }

        [HttpDelete("/api/Country/{id}")]
        public void DeleteCountry(int id)
        {
            if (!_countryService.Remove(id))
            {
                Response.StatusCode = 200;
            }

            Response.StatusCode = 400;
        }
    }
}


//Code 201 created / code 400 bad request(validation failed) / code 500 database failed to create person
//Returformat int kan bytas mot IActionResult och return BadRequest();
//Code 200 was removed / code 404 not found / code 500 database failed to delete person
