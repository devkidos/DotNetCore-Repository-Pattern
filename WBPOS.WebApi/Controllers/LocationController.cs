using WBPOS.Services.Contracts;
using WBPOS.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WBPOS.WebApi.Controllers
{ 
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILogger<LocationController> _logger;
        private IServiceWrapper service;
        public LocationController(ILogger<LocationController> logger, IServiceWrapper _service)
        {
            service = _service;
            _logger = logger;
        }
       
        [HttpPost]
        [Route("Location/getlocation1")]
        public async Task<IActionResult> GetLocation(Location location)
        {
            RootObject rootObject = service.GeoLocation.GetLocation(location.Latitude, location.Longitude);
           // var data = "Full Address " + rootObject.display_name;
            
            return Ok(rootObject);
        }

        [HttpPost]
        [Route("Location/getlocation2")]
        public async Task<IActionResult> GetLocation2(Location location)
        {
            var data = await service.GeoLocation.GetLocation2(location.Latitude, location.Longitude);
            
            return Ok(data);
        }

       

    }
}
