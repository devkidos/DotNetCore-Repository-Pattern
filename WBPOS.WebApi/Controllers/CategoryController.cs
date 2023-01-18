using WBPOS.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace WBPOS.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private IServiceWrapper service;
        public CategoryController(ILogger<CategoryController> logger, IServiceWrapper _service)
        {
            service = _service;
            _logger = logger;
        }

        [HttpGet]
        [Route("Category/All")]
        public async Task<IActionResult> GetAll()
        {
            var data = await service.Categories.GetList();
            return Ok(data);
        }
    }
}
