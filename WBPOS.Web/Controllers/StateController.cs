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
    public class StateController : AppController
    {
        private readonly ILogger<StateController> _logger;
        private IServiceWrapper service;
        public StateController(ILogger<StateController> logger, IServiceWrapper _service)
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
            var datalist = await service.State.GetStateList(param);

            if (datalist.Exceptions.Count > 0)
                return Json(new { message = "There is something wrong" });

            return Json(new { draw = param.Draw, recordsFiltered = datalist.Datas.recordsTotal, recordsTotal = datalist.Datas.recordsTotal, data = datalist.Datas.data.ToList() });
        }

        public async Task<ActionResult> Create()
        {
            var data = await service.Country.GetCountryListForDDL();
            var model = new VMState();
            model.countryList = data.Datas;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(VMState model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userid = CurrentUser.UserID;
                    var data = await service.State.Insert(model, userid);

                    if (data.Success)
                    {
                        this.AddAlertSuccess($"{model.stateName} created successfully.");
                        return RedirectToAction(nameof(Index), new { listId = model.stateName });
                    }
                    else
                    {
                        this.AddAlertDanger($"{model.stateName} already exist.");
                        return RedirectToAction(nameof(Index), new { listId = model.stateName });
                    }
                }

                var list = await service.Country.GetCountryListForDDL();
                model.countryList = list.Datas;
                return View(model);
            }
            catch (Exception ex)
            {
                var data = await service.Country.GetCountryListForDDL();
                model.countryList = data.Datas;
                return View(model);
            }
        }

        public ActionResult Edit(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //var data = BLCountry.GetDataById(id).Data;
            var data = "";
            //if (data == null)
            //{
            //    return HttpNotFound();
            //}
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, int data)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                //    country.UpdatedBy = null;
                //    country.UpdatedDate = DateTime.Now;
                //    var data = BLCountry.Save(country, "update");
                //    if (data.Exceptions.Count > 0)
                //    {
                //        string error = "";
                //        foreach (var keyValuePair in data.Exceptions)
                //        {
                //            error += keyValuePair.Value;
                //        }
                //        return View(country).WithError(error);
                //    }
                //    return RedirectToAction("Index").WithSuccess("Country updated successfully.");
                //}
                return View(data); //.WithError("Model is not valid !");
            }
            catch (Exception ex)
            {
                //ErrorLog.LogInsert(ex, "General", CurrentUser.Name, ControllerContext.RouteData.Values["controller"].ToString(), ControllerContext.RouteData.Values["action"].ToString());
                return View(data);//.WithError(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult DeleteData(Guid id)
        {
            //VMCountry country = new VMCountry();
            //if (ModelState.IsValid)
            //{
            //    country.CountryId = new Guid(id.ToString());
            //    country.Status = "Active";

            //    var data = BLCountry.Delete(country);
            //    if (data.Exceptions.Count > 0)
            //    {
            //        string error = "";
            //        foreach (var keyValuePair in data.Exceptions)
            //        {
            //            error += keyValuePair.Value;
            //        }
            //        return RedirectToAction("Index").WithError(error);
            //    }
            //}
            return Json(new { value = "success" });
        }
    }
}
