using WBPOS.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WBPOS.Web.Controllers
{
    public class CommonController : Controller
    {
         
        private IServiceWrapper service; 

        public CommonController(IServiceWrapper _service)
        {
            service = _service;
        }
        
    }
}
