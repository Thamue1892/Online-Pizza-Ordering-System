using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnlinePizzaOrderingSystem.DataAccess.Data.IRepository;
using OnlinePizzaOrderingSystem.Models;

namespace OnlinePizzaOrderingSystem.DataAccess.Data
{
    public class PizzaRepository:Repository<Pizza>,IPizzaRepository
    {
        private readonly ApplicationDbContext _db;

        public PizzaRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(Pizza pizza)
        {
            var objFromDb = _db.Pizza.FirstOrDefault(p => p.Id == pizza.Id);

            if (objFromDb != null)
            {
                objFromDb.Name = pizza.Name;
                objFromDb.Price = pizza.Price;
                objFromDb.Category = pizza.Category;
                objFromDb.Description = pizza.Description;
                objFromDb.ImageUrl = pizza.ImageUrl;
                objFromDb.CategoryId = pizza.CategoryId;
                objFromDb.IsPizzaOfTheWeek = pizza.IsPizzaOfTheWeek;
            }

            _db.SaveChanges();
        }
    }
}
