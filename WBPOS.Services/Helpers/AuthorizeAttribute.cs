using WBPOS.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;


//namespace WBPOS.Services.Helpers
//{
//    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
//    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
//    {
//        public void OnAuthorization(AuthorizationFilterContext context)
//        {
//            var user = (VMUser)context.HttpContext.Items["User"];
//            var test = ((Microsoft.AspNetCore.Http.DefaultHttpContext)context.HttpContext).User;
//            if (user == null)
//            {
//                // not logged in
//                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
//            }
//        }
//    }
//}
