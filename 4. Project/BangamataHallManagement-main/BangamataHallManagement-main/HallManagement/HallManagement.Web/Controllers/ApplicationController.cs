using HallManagement.Model;
using HallManagement.Model.Entities;
using HallManagement.Model.ViewModels;
using HallManagement.Service;
using HallManagement.Service.Interfaces;
using HallManagement.Web.Classes;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Web.Helpers;
using System.Xml.Linq;
using Web.Classes;


namespace HallManagement.Web.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly IApplicationService _applicationService;
        private IOptions<_AppSettings> _settings;
        private readonly ILogger<StudentController> _logger;
        private readonly BangamataHallContext _banamataHallContext;
        // GET: ApplicationController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ApplicationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ApplicationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ApplicationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ApplicationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ApplicationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ApplicationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ApplicationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
