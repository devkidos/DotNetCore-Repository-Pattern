using WBPOS.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace WBPOS.WebApi.Controllers
{
    [ApiController] 
    public class CountryController : ControllerBase
    {
        private readonly ILogger<CountryController> _logger;
        private IServiceWrapper service;
        public CountryController(ILogger<CountryController> logger, IServiceWrapper _service) 
        {
            service = _service;
            _logger = logger;
        }
         
        [HttpGet]
        [Route("Country/All")]
        public async Task<IActionResult> GetAll()
        {
            var data = await service.Country.GetData();
            return Ok(data);
        }

        [HttpGet]
        [Route("Country/id/{id}")]
        public async Task<IActionResult> GetById(decimal id)
        {
            var data = await service.Country.GetDataById(id);
            return Ok(data);
        }
    }
}
