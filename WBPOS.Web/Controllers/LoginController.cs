using WBPOS.Services.Contracts;
using WBPOS.ViewModel;
using WBPOS.ViewModel.Request;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WBPOS.Web.Controllers
{
    public class LoginController : Controller
    {
        private IServiceWrapper service;

        public LoginController(IServiceWrapper _service)
        {
            service = _service;
        }
        public IActionResult Index(string ReturnUrl = "/")
        {
            VMLogin objLoginModel = new VMLogin();
            objLoginModel.ReturnUrl = ReturnUrl;
            return View(objLoginModel);
        }
        [HttpPost]
        public async Task<IActionResult> Index(VMLogin model)
        {
            if (ModelState.IsValid)
            {
                var _userData = new AuthenticateRequest
                {
                    Username = model.UserName, Password = model.Password
                };
                var user = await service.User.Authenticate(_userData, "admin");
                if (user.Datas == null)
                {
                    //Add logic here to display some message to user
                    ViewBag.Message = "Username or Password is invalid.";
                    return View(model);
                }
                else
                {
                    //A claim is a statement about a subject by an issuer and
                    //represent attributes of the subject that are useful in the context of authentication and authorization operations.
                    var claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier,Convert.ToString(user.Datas.userId)),
                    new Claim(ClaimTypes.Name,user.Datas.username),
                    new Claim(ClaimTypes.Role,"Admin"),
                    new Claim("Id",user.Datas.userId.ToString())
                    };
                    //Initialize a new instance of the ClaimsIdentity with the claims and authentication scheme
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    //Initialize a new instance of the ClaimsPrincipal with ClaimsIdentity
                    var principal = new ClaimsPrincipal(identity);
                    //SignInAsync is a Extension method for Sign in a principal for the specified scheme.
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        principal, new AuthenticationProperties() { IsPersistent = model.RememberLogin });

                    return LocalRedirect(model.ReturnUrl);
                }
            }
            return View(model);
        }
        public async Task<IActionResult> LogOut()
        {
            //SignOutAsync is Extension method for SignOut
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //Redirect to home page
            return LocalRedirect("/");
        }
    }
}
