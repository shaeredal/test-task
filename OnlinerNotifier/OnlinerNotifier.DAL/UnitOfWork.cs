using System;
using OnlinerNotifier.DAL.Models;
using OnlinerNotifier.DAL.Repositories;
using OnlinerNotifier.DAL.Repositories.Interfaces;

namespace OnlinerNotifier.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private Context db = new Context();
        private UserRepository userRepository;
        private ProductRepository productRepository;
        private ProductPriceChangeRepository productPriceChangeRepository;
        private UserProductRepository userProductRepository;

        public IUserRepository Users
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(db);
                }
                return userRepository;
            }
        }

        public IRepository<Product> Products
        {
            get
            {
                if (productRepository == null)
                {
                    productRepository = new ProductRepository(db);
                }
                return productRepository;
            }
        }

        public IRepository<ProductPriceChange> PriceCanges
        {
            get
            {
                if (productPriceChangeRepository == null)
                {
                    productPriceChangeRepository = new ProductPriceChangeRepository(db);
                }
                return productPriceChangeRepository;
            }
        }

        public IRepository<UserProduct> UserProducts
        {
            get
            {
                if (userProductRepository == null)
                {
                    userProductRepository = new UserProductRepository(db);
                }
                return userProductRepository;
            }
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
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
