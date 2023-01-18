using WBPOS.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace WBPOS.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ILogger<CMSController> _logger;
        private IServiceWrapper service;
        public SettingsController(ILogger<CMSController> logger, IServiceWrapper _service)
        {
            service = _service;
            _logger = logger;
        }

        [HttpGet]
        [Route("General/Settings")]
        public async Task<IActionResult> GetAll()
        {
            var data = await service.Settings.GetSettingsList();
            return Ok(data);
        }
    }
}
