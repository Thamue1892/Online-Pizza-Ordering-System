using System;
using System.Collections.Generic;
using System.Text;

namespace OnlinePizzaOrderingSystem.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Pizza> PizzasOfTheWeek { get; set; }
    }
}
