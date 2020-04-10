using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TakerDataClient.Interface.IProviders;
using TakerDataClient.Models;
using TakerDataClient.Models.Forms;
using WebTakerData.Interface.ICore;

namespace TakerDataClient.Controllers
{
    public class HomeController : Controller
    {

        private readonly IFormsProvider _formProvider;
        public readonly ISettings _appSettings;
        private readonly ILogger<Controller> _logger;
        private readonly IHostingEnvironment _hostingEnvironment;



        public HomeController(IFormsProvider formProvider, ISettings appSettings, IServices services,
            ILogger<Controller> logger,
            IHostingEnvironment hostingEnvironment)
        {
            _formProvider = formProvider;
            _appSettings = appSettings;
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }


        public IActionResult Index() => View();


        [HttpPost]
        public JsonResult FormOnloadAsync([FromBody] TokenDTO oToken)
        {
            JsonResponseGetForm result = new JsonResponseGetForm();
            if (oToken != null)
            {

                JsonRequestGetForm oRequest = new JsonRequestGetForm();
                oRequest.Token = oToken.token;
                result = _formProvider.GetUserForm(oRequest).Result;

            }
            return Json(new
            {
                success = result.success,
                code = result.code,
                message = result.message,
                ResponseGetForm = result.result
            });
        }


        [HttpPost]
        public JsonResult FormSave([FromBody] JsonRequestSaveForm form)
        {

            bool success = false;
            JsonResponseSaveForm result = new JsonResponseSaveForm();
            try
            {
                if (form != null)
                {
                    result = _formProvider.SaveUserResponses(form).Result;
                    success = result.success;
                }
               
            }
            catch (Exception ex)
            {

                success = false;
                _logger.LogError(default(EventId), ex, ex.Message);
            }

            return Json(new
            {
                success = success,
                code = result.code,
                message = result.message,
                ResponseGetForm = result.result
            });
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
