using E_Coffee.Models;
using E_Coffee.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Coffee.Infrastructure
{
    public static class Mapper
    {
        public static Product Map(ProductModel model)
        {
            var product = new Product
            {
                Category = model.Category,
                Description = model.Description,
                Image = model.Image,
                Name = model.Name,
                Price = model.Price,
                ProductID = model.ProductID
            };
            return product;
        }

        public static ProductModel Map(Product product)
        {
            var model = new ProductModel
            {
                Category = product.Category,
                Description = product.Description,
                Image = product.Image,
                Name = product.Name,
                Price = product.Price,
                ProductID = product.ProductID
            };
            return model;
        }
    }
}
