using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MultiThreadingWebCoreDemo.Models;
using MultiThreadingWebCoreDemo.Repository;
using MultiThreadingWebCoreDemo.Utility;

namespace MultiThreadingWebCoreDemo.Controllers
{
    public class UserController : Controller
    {
        private readonly IOptions<MySettingsModel> appSettings;

        public UserController(IOptions<MySettingsModel> app)
        {
            appSettings = app;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult Test()
        {
            var isDone = true;
            var msg = "Process submitted";
            Task.Factory.StartNew(() =>
            {
                var conStr = appSettings.Value.DbConn;
                var client = DbClientFactory<MultiprocessDbClient>.Instance;
                var id = client.InsertMultiprocessStatus(conStr, "Ravi", "User", 50);
                var totalRecords = 50;
                var curr = 1;
                int success = 0, fail = 0;
                for (int i = 0; i < totalRecords; i++)
                {
                    Thread.Sleep(500);
                    decimal percentage = Convert.ToDecimal((Convert.ToDecimal(curr) / Convert.ToDecimal(totalRecords))) * Convert.ToDecimal(100);
                    success++;
                    curr++;
                    client.UpdateMultiprocessStatus(conStr, id, percentage, false, fail, success);
                }
            });
            var obj = new { isSuccess = isDone, returnMessage = msg };
            return Json(obj);
        }

        [HttpPost]
        public JsonResult GetMultiProcessStatus([FromBody]MultiprocessModel model)
        {
            var data = DbClientFactory<MultiprocessDbClient>.Instance.GetMultiprocessStatus(
                appSettings.Value.DbConn, model.UserId,model.Module);
            var response = new Message<MultiprocessModel>();
            response.IsSuccess = true;
            response.Data = data;
            return Json(response);
        }
    }
}