using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspAppMvcWithDb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Detail()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}
