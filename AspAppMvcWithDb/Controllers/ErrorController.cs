using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;

namespace AspAppMvcWithDb.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        }

        [Route("Error/{statuscode}")]
        public IActionResult Index(int status)
        {
            switch (status)
            {
                case 404:
                    ViewBag.ErrorMessage = "on ne parvient pas à traiter votre requête";
                    logger.LogWarning($"try to acces a endpoint that doesn't exist \n Path : {HttpContext.Request.Path} ");
                    break;
              
            }
            return View("ErrorGlobal");
        }
        

        [Route("/Error")]
        public IActionResult Error()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            logger.LogError($"Path : {exception.Path} \n Message : {exception.Error.Message}");

            return View("Error500");
        }
    }
}
