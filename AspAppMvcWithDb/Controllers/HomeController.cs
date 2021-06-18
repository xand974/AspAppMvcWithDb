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
            if (ModelState.IsValid)
            {
                string fileName = UpdatePhoto
                    (model);

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

        private string UpdatePhoto(CreateViewModel model)
        {
            string fileName = "";
            //path vers le dossier images
            string newFile = Path.Combine(Environment.WebRootPath, "Images");

            //mettre les fichiers images uniques en ajoutat un Guid
            fileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;

            //Combine le path vers le dossier Images + le fichier 
            string filepath = Path.Combine(newFile, fileName);

            //Copier la photo dans le dossier après Post
            using(FileStream fileStream = new(filepath, FileMode.Create))
            {
                model.Photo.CopyTo(fileStream);
            }
            return fileName;
        }

        public IActionResult Detail(int? id)
        {
            Post postFound = _management.GetPostById(id.Value);
            if (postFound == null)
            {
                int code = Response.StatusCode = 404;
                return View("HomeError", code);
            }

            var model = new DetailViewModel()
            {
                GetPostById = _management.GetPostById(id??1)
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
            Post postFound = _management.GetPostById(id);

            _management.Delete(postFound);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Post postFound = _management.GetPostById(id);
            if(postFound == null)
            {
                int error = Response.StatusCode = 404;
                return View("HomeError", error);
            }
            var model = new EditViewModel()
            {
                Id = postFound.Id,
                Title = postFound.Title,
                Description = postFound.Description,
                PhotoPath = postFound.Photo,
                
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(EditViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                var postFound = _management.GetPostById(model.Id);

                postFound.Title = model.Title;
                postFound.Description = model.Description;
                if(model.Photo != null)
                {
                    if(model.PhotoPath != null)
                    {
                        //suppression Photo
                        //recup path vers la photo
                        string filePath = Path.Combine(Environment.WebRootPath, "Images", model.PhotoPath);
                       
                        //supprimer photo
                        System.IO.File.Delete(filePath);
                    }
                    postFound.Photo = UpdatePhoto(model);
                }

                _management.Update(postFound);  
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
