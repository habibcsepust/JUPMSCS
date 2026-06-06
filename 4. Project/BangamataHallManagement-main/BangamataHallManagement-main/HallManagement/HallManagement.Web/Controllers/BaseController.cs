using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HallManagement.Web.Controllers
{
    public class BaseController : Controller
    {
        protected Int32 _userId;
        protected Int32 _roleId;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _userId = HttpContext.Session.GetInt32("Id") != null ? (Int32)HttpContext.Session.GetInt32("Id") : 0;
            _roleId = HttpContext.Session.GetInt32("RoleId") != null ? (Int32)HttpContext.Session.GetInt32("RoleId") : 0;

            base.OnActionExecuting(filterContext);
            if (_userId == 0)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Home" }, { "action", "Index" } });
            }

            if (_roleId == 3)
            {
                string controllerName = filterContext.RouteData.Values["controller"].ToString();
                string actionName = filterContext.RouteData.Values["action"].ToString();
                var menuAccess = HttpContext.Session.GetString("MenuAccess");
                if (menuAccess != null && !menuAccess.Contains($"{controllerName}/{actionName}"))
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Error" }, { "action", "Index" } });
                }
            }
        }
    }
}
