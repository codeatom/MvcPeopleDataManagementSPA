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
    [Authorize]
    public class LanguageController : Controller
    {
        private readonly ILanguageService _languageService;

        public LanguageController(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        public ActionResult Index()
        {
            return View(_languageService.All());
        }   

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateLanguage createLanguage)
        {
            if(ModelState.IsValid)
            {
                _languageService.Add(createLanguage);
                return RedirectToAction(nameof(Index));
            }           
            
            return View(createLanguage);        
        }

        public ActionResult Edit(int id)
        {
            Language language = _languageService.FindById(id);
            EditLanguage editLanguage = new EditLanguage();

            if (language == null)
            {
                return RedirectToAction("Index");
            }

            editLanguage.Id = id;
            editLanguage.CreateLanguage = _languageService.LanguageToCreateLanguage(language);

            return View(editLanguage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CreateLanguage createLanguage)
        {
            EditLanguage editLanguage = new EditLanguage();

            if (ModelState.IsValid)
            {
                Language language = _languageService.Edit(id, createLanguage);
                return RedirectToAction(nameof(Index));
            }

            editLanguage.Id = id;
            editLanguage.CreateLanguage = createLanguage;

            return View(editLanguage);
        }

        public ActionResult DeleteRequest(int id)
        {
            LanguageViewModel languageViewModel = new LanguageViewModel();
            languageViewModel.Language = _languageService.FindById(id);
            languageViewModel.LanguageIsAttached = _languageService.LanguageIsConnected(id);

            return View(languageViewModel);
        }

        public IActionResult Delete(int id)
        {
            Language language = _languageService.FindById(id);

            if (language != null)
            {
                _languageService.Remove(id);
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
