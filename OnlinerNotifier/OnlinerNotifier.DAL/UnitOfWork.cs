﻿using System;
using OnlinerNotifier.DAL.Repositories;

namespace OnlinerNotifier.DAL
{
    public class UnitOfWork : IDisposable
    {
        private Context db = new Context();
        private UserRepository userRepository;
        private ProductRepository productRepository;
        private ProductPriceChangeRepository productPriceChangeRepository;
        private UserProductRepository userProductRepository;

        public UserRepository Users
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

        public ProductRepository Products
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

        public ProductPriceChangeRepository PriceCanges
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

        public UserProductRepository UserProducts
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
