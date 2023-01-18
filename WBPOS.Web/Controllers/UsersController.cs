using DevKido.Utilities.Core.DataTable;
using WBPOS.Services.Contracts;
using WBPOS.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace WBPOS.Web.Controllers
{
    [Authorize]
    public class UsersController : AppController
    {
        private readonly ILogger<UsersController> _logger;
        private IServiceWrapper service;

        public UsersController(ILogger<UsersController> logger, IServiceWrapper _service)
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
            var datalist = await service.User.GetUsersList(param);

            if (datalist.Exceptions.Count > 0)
                return Json(new { message = "There is something wrong" });

            return Json(new { draw = param.Draw, recordsFiltered = datalist.Datas.recordsTotal, recordsTotal = datalist.Datas.recordsTotal, data = datalist.Datas.data.ToList() });
        }

        public ActionResult Create()
        {
            return View();
        }        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int country)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //country.CountryId = Guid.NewGuid();
                    //country.Status = "Active";
                    //country.CreatedBy = Guid.NewGuid();
                    //country.CreatedDate = DateTime.Now;
                    //country.UpdatedBy = null;
                    //country.UpdatedDate = DateTime.Now;

                    //var data = BLCountry.Save(country, "add");
                    //if (data.Exceptions.Count > 0)
                    //{
                    //    string error = "";
                    //    foreach (var keyValuePair in data.Exceptions)
                    //    {
                    //        error += keyValuePair.Value;
                    //    }
                    //    //ModelState.AddModelError("", error);
                    //    return View(country).WithError(error);
                    //}

                    return RedirectToAction("Index");//.WithSuccess("Country added successfully.");
                }

                return View(country);//.WithError("Model is not valid !");
            }
            catch (Exception ex)
            {
                //ErrorLog.LogInsert(ex, "General", CurrentUser.Name, ControllerContext.RouteData.Values["controller"].ToString(), ControllerContext.RouteData.Values["action"].ToString());
                return View(country);//.WithError(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Edit(Guid Id)
        {

            string UserId = Id.ToString();

            if (Id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var data = await service.User.GetUserData(UserId);
            //var data = "";
            if (data == null)
            {
                //return HttpNotFound();
            }
            data.Datas.statusList = await CommonMethods.GetStatusList2();

            return View(data.Datas);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(VMUsers model)
        {
            var userid = CurrentUser.UserID;
            var data = await service.User.UpdateUserData(model, userid);

            this.AddAlertSuccess($"{model.userId} updated successfully.");
            return RedirectToAction(nameof(Index), new { listId = model.userId });
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
