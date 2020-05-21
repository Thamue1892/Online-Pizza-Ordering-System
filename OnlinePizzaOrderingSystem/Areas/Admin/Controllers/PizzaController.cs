using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlinePizzaOrderingSystem.DataAccess.Data.IRepository;

namespace OnlinePizzaOrderingSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PizzaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PizzaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }




        #region API CALLS

        public IActionResult GetAll()
        {
            return Json(new {data = _unitOfWork.Pizza.GetAll()});
        }

        #endregion
    }
}