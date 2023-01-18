using WBPOS.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace WBPOS.WebApi.Controllers
{ 
    [ApiController]
    public class CMSController : ControllerBase
    {
        private readonly ILogger<CMSController> _logger;
        private IServiceWrapper service;
        public CMSController(ILogger<CMSController> logger, IServiceWrapper _service)
        {
            service = _service;
            _logger = logger;
        }

        [HttpGet]
        [Route("CMS/All")]
        public async Task<IActionResult> GetAll()
        {
            var data = await service.CMS.CMSList();
            return Ok(data);
        }
    }
}
