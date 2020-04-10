using System;
using System.Linq;
using System.Threading.Tasks;
using WebTakerData.Core;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using WebTakerData.Interface.ICore;


namespace WebTakerData.Base
{
    [Authorize]
    public class BaseControllerAuthorize : Controller
    {
        public readonly ISettings _appSettings;
        private readonly ILogger<Controller> _logger;

        public BaseControllerAuthorize(ISettings _settings,  ILogger<Controller> logger)
        {
            _logger = logger;
            _appSettings = _settings;
          
        }

        protected string ConeccionToken { get; private set; }
        protected string CodigoUsuario { get; private set; }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            try
            {

               

             

                bool isAjaxCall = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
                if (isAjaxCall)
                {
                    base.OnActionExecutionAsync(context, next);
                    return Task.FromResult(0);
                }

              
                
            }
            catch (Exception ex)
            {
                _logger.LogError(default(EventId), ex, ex.Message  + ex.StackTrace);
               // throw;
            }
            return base.OnActionExecutionAsync(context, next);

        }

     

        private void CargarViewBag(string token, string codigo)
        {
         
            ViewBag.IdPerfil = "1";
            ViewBag.PerfilDescripcion = "Administrador";
            ViewBag.CodigoUsuario = codigo;
            ViewBag.ConeccionToken = token;
       
        }

    

        public async Task<T> GetSession<T>(string key)
        {
            return await HttpContext.Session.GetData<T>(key);
        }

        public async Task SetSession(string key, object value)
        {
            await HttpContext.Session.SetData(key, value);
        }
    }
}
