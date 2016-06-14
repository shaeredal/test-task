using System;
using System.Collections.Generic;
using System.Data.Entity;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.DAL.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private Context db;

        public ProductRepository(Context context)
        {
            this.db = context;
        }

        public IEnumerable<Product> GetAll()
        {
            return db.Products;
        }

        public Product Get(int id)
        {
            return db.Products.Find(id);
        }

        public void Create(Product item)
        {
            db.Products.Add(item);
        }

        public void Update(Product item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Product book = db.Products.Find(id);
            if (book != null)
            {
                db.Products.Remove(book);
            }
        }
    }
}
