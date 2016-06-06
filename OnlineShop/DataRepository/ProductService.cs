using System;
using OnlineShop;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.DataRepository
{
    public class ProductService : IProductService
    {
        static List<Product> products = new List<Product>();
        
        static ProductService()
        {
            products.Add(new Product { ItemCode = "A", Price = 50 });
            products.Add(new Product { ItemCode = "B", Price = 30 });
            products.Add(new Product { ItemCode = "C", Price = 20 });
            products.Add(new Product { ItemCode = "D", Price = 15 });
            products.Add(new Product { ItemCode = "E", Price = 10 });
        }

        public Product GetProductByProductCode(string productCode)
        {
            var product = products.Where(x => x.ItemCode == productCode).FirstOrDefault();
            if (product == null)
                throw new KeyNotFoundException("Product code not found in the product list");

            return product;
        }
    }
}