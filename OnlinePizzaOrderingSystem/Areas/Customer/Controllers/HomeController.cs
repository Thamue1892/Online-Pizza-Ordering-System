using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OnlinePizzaOrderingSystem.DataAccess.Data.IRepository;
using OnlinePizzaOrderingSystem.Models;
using OnlinePizzaOrderingSystem.Models.ViewModels;

namespace OnlinePizzaOrderingSystem.Areas.Customer.Controllers
{
   [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                PizzaList = _unitOfWork.Pizza.GetAll(includeProperties:"Category")
            };
            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
