using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;

namespace AspAppMvcWithDb.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statuscode}")]
        public IActionResult Index(int status)
        {
            switch (status)
            {
                case 404:
                    ViewBag.ErrorMessage = "on ne parvient pas à traiter votre requête";
                    break;
              
            }
            return View("ErrorGlobal");
        }
        

        [Route("/Error")]
        public IActionResult Error()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.ErrorPath = exception.Path;
            ViewBag.ErrorMessage = exception.Error.Message;

            return View("Error500");
        }
    }
}
