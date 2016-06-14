using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.DAL.Repositories
{
    class ProductRepository : IRepository<Product>
    {
        private Context db;

        public ProductRepository()
        {
            this.db = new Context();
        }

        public IEnumerable<Product> GetList()
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
                db.Products.Remove(book);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
