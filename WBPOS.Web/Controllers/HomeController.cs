using WBPOS.Web.Models;
using WBPOS.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WBPOS.Web.Controllers
{
    [Authorize]
    public class HomeController : AppController
    {
        private readonly ILogger<HomeController> _logger;
        private IServiceWrapper service;
        public HomeController(ILogger<HomeController> logger, IServiceWrapper _service)
        {
            _logger = logger;
            service = _service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
