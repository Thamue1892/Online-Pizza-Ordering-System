using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlinePizzaOrderingSystem.Models.ViewModels
{
    public class PizzaViewModel
    {
        public Pizza Pizza { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
