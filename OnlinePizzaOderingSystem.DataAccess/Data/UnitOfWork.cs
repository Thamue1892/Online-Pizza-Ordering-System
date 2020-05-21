using System;
using System.Collections.Generic;
using System.Text;
using OnlinePizzaOrderingSystem.DataAccess.Data.IRepository;

namespace OnlinePizzaOrderingSystem.DataAccess.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Pizza = new PizzaRepository(db);
        }

        public ICategoryRepository Category { get; private set; }

        public IPizzaRepository Pizza { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
