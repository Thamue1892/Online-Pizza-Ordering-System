using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        

        [BindProperty]
        public PizzaViewModel PizzaViewModel { get; set; }

        public PizzaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                if (PizzaViewModel.Pizza.Id==0)
                {
                    _unitOfWork.Pizza.Add(PizzaViewModel.Pizza);
                }
                else
                {
                    var pizzaFromDb = _unitOfWork.Pizza.Get(PizzaViewModel.Pizza.Id);  
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