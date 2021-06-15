using AspAppMvcWithDb.Models;
using AspAppMvcWithDb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspAppMvcWithDb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostManagement _management;

        public HomeController(IPostManagement management)
        {
            this._management = management;
        }

        public IActionResult Index()
        {
            var model = new IndexViewModel()
            {
                GetPosts = _management.GetPosts()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                _management.Create(post);
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Detail(int id)
        {
            var model = new DetailViewModel()
            {
                GetPostById = _management.GetPostById(id)
            };
            return new JsonResult(model.GetPostById);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _management.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
