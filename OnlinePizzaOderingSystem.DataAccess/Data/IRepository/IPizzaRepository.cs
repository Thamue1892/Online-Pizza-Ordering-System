using System;
using System.Collections.Generic;
using System.Text;
using OnlinePizzaOrderingSystem.Models;

namespace OnlinePizzaOrderingSystem.DataAccess.Data.IRepository
{
    public interface IPizzaRepository : IRepository<Pizza>
    {
        void Update(Pizza pizza);
    }
}
