using HallManagement.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace HallManagement.Web.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
