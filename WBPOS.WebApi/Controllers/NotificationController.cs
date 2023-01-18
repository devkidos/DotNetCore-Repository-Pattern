using WBPOS.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace WBPOS.WebApi.Controllers
{ 
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly ILogger<NotificationController> _logger;
        private IServiceWrapper service;
        public NotificationController(ILogger<NotificationController> logger, IServiceWrapper _service)
        {
            service = _service;
            _logger = logger;
        }

        [HttpGet]
        [Route("Notification/All")]
        public async Task<IActionResult> GetAll()
        {
            var data = await service.Notification.NotificationsList();
            return Ok(data);
        }
    }
}
