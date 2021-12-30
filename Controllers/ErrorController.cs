using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagment.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        }

        [Route ("Error/{statuscode}")]
        public IActionResult HttpStatusCodeHandler(int statuscode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statuscode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, The resource you requested coulden't be found";
                    logger.LogWarning($"404 Error occured. path :{statusCodeResult.OriginalPath} and Query string ={statusCodeResult.OriginalQueryString}");
                    break;
            }
            return View("NotFound");
        }
        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceptiondetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            logger.LogError($"The Path {exceptiondetails.Path} throw an exception {exceptiondetails.Error}");
            return View("Error");
        }
    }
}
 