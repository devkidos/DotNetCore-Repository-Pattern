using DevKido.Utilities.Core.DataTable;
using WBPOS.Services.Contracts;
using WBPOS.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WBPOS.Web.Controllers
{
    [Authorize]
    public class CountryController : AppController
    {
        private readonly ILogger<CountryController> _logger;
        private IServiceWrapper service;

        public CountryController(ILogger<CountryController> logger, IServiceWrapper _service)
        {
            service = _service;
            _logger = logger;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> GetJsonData(DTParameters param)
        {
            var datalist = await service.Country.GetCountryList(param);

            if (datalist.Exceptions.Count > 0)
                return Json(new { message = "There is something wrong" });

            return Json(new { draw = param.Draw, recordsFiltered = datalist.Datas.recordsTotal, recordsTotal = datalist.Datas.recordsTotal, data = datalist.Datas.data.ToList() });
        }

        [HttpPost]
        public async Task<ActionResult> Create(VMCountry model)
        {
            var userid = CurrentUser.UserID;

            var data = await service.Country.Insert(model,userid);

            if (data.Success)
            {
                this.AddAlertSuccess($"{model.countryName} created successfully.");
                return RedirectToAction(nameof(Index), new { listId = model.countryName });
            }
            else
            {
                this.AddAlertDanger($"{model.countryName} already exist.");
                return RedirectToAction(nameof(Index), new { listId = model.countryName });
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Edit(decimal? Id)
        {
            if (Id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var data = await service.Country.GetDataById(Id);
            //var data = "";
            if (data == null)
            {
                //return HttpNotFound();
            }
            data.Datas.statusList = await CommonMethods.GetStatusList2();
            return View(data.Datas);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(VMCountry model)
        {
            var userid = CurrentUser.UserID;

            var data = await service.Country.Update(model, userid);

            this.AddAlertSuccess($"{model.countryName} updated successfully.");
            return RedirectToAction(nameof(Index), new { listId = model.countryName });
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(Guid Id)
        {
            var data = await service.Country.Delete(Id);
            return Json(new { value = "success" });
        }

        [HttpPost]
        public async Task<ActionResult> DeleteData(decimal? id)
        {
            var model = await service.Country.Delete(id);

            if (model.Exceptions.Count > 0)
            {
                string error = "";
                foreach (var keyValuePair in model.Exceptions)
                {
                    error += keyValuePair.Value;
                }
                this.AddAlertSuccess($"{error}");
                return RedirectToAction(nameof(Index));
            }

            return Json(new { value = "success" });
        }
    }
}
