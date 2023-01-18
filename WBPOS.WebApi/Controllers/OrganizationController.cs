using WBPOS.Services.Contracts;
using WBPOS.Services.Helpers;
using WBPOS.ViewModel;
using WBPOS.ViewModel.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WBPOS.WebApi.Controllers
{ 
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly ILogger<OrganizationController> _logger;
        private IServiceWrapper service;
        public OrganizationController(ILogger<OrganizationController> logger, IServiceWrapper _service)
        {
            service = _service;
            _logger = logger;
        }

        [HttpPost]
        [Route("Organization/Register")]
        public async Task<IActionResult> RegisterOrganization(VMOrganizations model)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst("Id")?.Value;

            var data = await service.Organizations.Register(model, userId);
            return Ok(data);
        }
    }
}
