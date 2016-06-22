using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
            return db.Products.Include(prod => prod.Users).SingleOrDefault(x => x.Id == id);
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
            Product product = db.Products.Find(id);
            if (product != null)
            {
                db.Products.Remove(product);
            }
        }
    }
}
