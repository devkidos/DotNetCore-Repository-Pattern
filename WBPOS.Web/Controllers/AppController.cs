using WBPOS.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Security.Claims;

namespace WBPOS.Web.Controllers
{
    public class AppController : Controller
    { 
        public AppUser CurrentUser
        {
            get
            {
                return new AppUser(this.User as ClaimsPrincipal);
            }
        }

        //protected override void OnException(ExceptionContext filterContext)
        //{
        //    Exception exception = filterContext.Exception;
        //    //Logging the Exception
        //    filterContext.ExceptionHandled = true;
        //    string actionName = this.ControllerContext.RouteData.Values["action"].ToString(); // filterContext.RouteData.Values["action"].ToString()
        //    string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString(); //filterContext.RouteData.Values["controller"].ToString()

        //    ErrorLog.LogInsert(exception, "General", CurrentUser.Name, controllerName, actionName);

        //    var Result = this.View("Error", new HandleErrorInfo(exception,
        //        filterContext.RouteData.Values["controller"].ToString(),
        //        filterContext.RouteData.Values["action"].ToString()));

        //    filterContext.Result = Result;

        //}
    }
}
