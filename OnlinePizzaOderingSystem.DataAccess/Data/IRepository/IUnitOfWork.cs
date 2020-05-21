using System;
using System.Collections.Generic;
using System.Text;

namespace OnlinePizzaOrderingSystem.DataAccess.Data.IRepository
{
    public interface IUnitOfWork:IDisposable
    {
        ICategoryRepository Category { get; }
        IPizzaRepository Pizza { get; }

        void Save();
    }
}
