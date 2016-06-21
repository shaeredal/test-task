﻿using OnlinerNotifier.BLL.Models;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Mappers
{
    public class ProductMapper
    {
        public Product ToDomain(ProductViewModel model)
        {
            return new Product()
            {
                OnlinerId = model.OnlinerId,
                Name = model.Name,
                MaxPrice = model.MaxPrice,
                MinPrice = model.MinPrice
            };
        }

        public ProductViewModel ToModel(Product product)
        {
            return new ProductViewModel()
            {
                OnlinerId = product.OnlinerId,
                Name = product.Name,
                MaxPrice = product.MaxPrice,
                MinPrice = product.MinPrice
            };
        }
    }
}