using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OnlinePizzaOrderingSystem.DataAccess.Data.IRepository;
using OnlinePizzaOrderingSystem.Models;
using OnlinePizzaOrderingSystem.Models.ViewModels;

namespace OnlinePizzaOrderingSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PizzaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        [BindProperty]
        public PizzaViewModel PizzaViewModel { get; set; }

        public PizzaController(IUnitOfWork unitOfWork,IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Upsert(int? id)
        {
            PizzaViewModel = new PizzaViewModel()
            {
                Pizza = new Models.Pizza(),
                CategoryList = _unitOfWork.Category.GetCategoryListForDropDown()
            };
            if (id!=null)
            {
                PizzaViewModel.Pizza = _unitOfWork.Pizza.Get(id.GetValueOrDefault());
            }

            return View(PizzaViewModel);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (PizzaViewModel.Pizza.Id==0)
                {
                    //New Pizza
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\pizza");
                    var extension = Path.GetExtension(files[0].FileName);
                    using (var fileStreams = new FileStream(Path.Combine(uploads,fileName + extension),FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }

                    PizzaViewModel.Pizza.ImageUrl = @"\images\Pizzas\" + fileName + extension;
                    _unitOfWork.Pizza.Add(PizzaViewModel.Pizza);
                }
                else
                {
                    //Edit Pizza
                    var pizzaFromDb = _unitOfWork.Pizza.Get(PizzaViewModel.Pizza.Id);
                    if (files.Count>0)
                    {
                        string fileName = Guid.NewGuid().ToString();
                        var uploads = Path.Combine(webRootPath, @"images\pizza");
                        var extension_new = Path.GetExtension(files[0].FileName);

                        var imagePath = Path.Combine(webRootPath, pizzaFromDb.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }

                        using (var fileStreams = new FileStream(Path.Combine(uploads,fileName+extension_new),FileMode.Create))
                        {
                            files[0].CopyTo(fileStreams);
                        }

                        PizzaViewModel.Pizza.ImageUrl = @"\images\pizza\" + fileName + extension_new;
                    }
                    else
                    {
                        PizzaViewModel.Pizza.ImageUrl = pizzaFromDb.ImageUrl;
                    }
                    _unitOfWork.Pizza.Update(PizzaViewModel.Pizza);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(PizzaViewModel);
            }

           
        }




        #region API CALLS

        public IActionResult GetAll()
        {
            return Json(new {data = _unitOfWork.Pizza.GetAll(/*includeProperties:"Category"*/) });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var pizzaFromDb = _unitOfWork.Pizza.Get(id);
            if (pizzaFromDb==null)
            {
                return Json(new {success = false, message = "Error while deleting."});
            }

            _unitOfWork.Pizza.Remove(pizzaFromDb);
            _unitOfWork.Save();
            return Json(new {success = true, message = "Deleted successfully."});
        }

        #endregion
    }
}