using WBPOS.Services.Contracts;
using WBPOS.ViewModel;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace WBPOS.Web.Controllers
{
    public class CommonMethods
    {
        private readonly ILogger<CommonMethods> _logger;
        private IServiceWrapper service;
        public CommonMethods(ILogger<CommonMethods> logger, IServiceWrapper _service)
        {
            service = _service;
            _logger = logger;
        }
 

        public static async Task<Dictionary<string, string>> GetStatusList()
        {
            return await Task.Run(() =>
            {
                Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
                keyValuePairs.Add("Active", "Active");
                keyValuePairs.Add("Deactive", "Deactive");
                keyValuePairs.Add("Deleted", "Deleted");

                return keyValuePairs;
            });
        }
        public static async Task<List<ddlList>> GetStatusList2()
        {
            return await Task.Run(() =>
            {
                List<ddlList> ddlLists = new List<ddlList>
                {
                    new ddlList{ DispalyText = "Active", ValueText = "Active"},
                    new ddlList{ DispalyText = "Deactive", ValueText = "Deactive"},
                    new ddlList{ DispalyText = "Deleted", ValueText = "Deleted"},
                };

                return ddlLists;
            });
        }
    }

   
}
