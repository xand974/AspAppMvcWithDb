using AspAppMvcWithDb.Models;
using AspAppMvcWithDb.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AspAppMvcWithDb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostManagement _management;

        public IWebHostEnvironment Environment { get; }

        public HomeController(IPostManagement management, IWebHostEnvironment environment)
        {
            this._management = management;
            Environment = environment;
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
        public IActionResult Create(CreateViewModel model)
        {
            string fileName = "";
            if (ModelState.IsValid)
            {
                //path vers le dossier images
                string newFile = Path.Combine(Environment.WebRootPath, "Images");
                
                //mettre les fichiers images uniques en ajoutat un Guid
                fileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                
                //Combine le path vers le dossier Images + le fichier 
                string filepath = Path.Combine(newFile, fileName);

                //Copier la photo dans le dossier après Post
                model.Photo.CopyTo(new FileStream(filepath, FileMode.Create));

                Post post = new()
                {
                    Title = model.Title,
                    Description = model.Description,
                    Photo = fileName
                };
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
