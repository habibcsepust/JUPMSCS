using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HallManagement.Web.Classes
{
    public class CustomAuthorizeAttribute : TypeFilterAttribute
    {
        // use comma separated roles for authorizing different roles
        public CustomAuthorizeAttribute(string roles) : base(typeof(AuthorizeActionFilter))
        {
            Arguments = new object[] { roles };
        }
    }

    public class AuthorizeActionFilter : IAuthorizationFilter
    {
        private readonly string[] _roles;
        public AuthorizeActionFilter(string roles)
        {
            _roles = roles.Split(',').Select(x => x.Trim()).ToArray();
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool isAuthorized = _roles.Contains(context.HttpContext.Session.GetString("userType"));
            if (!isAuthorized)
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Error" }, { "action", "Index" } });
        }
    }
}
