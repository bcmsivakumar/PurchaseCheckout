using OnlineShop.DataRepository;
using System;
using System.Collections.Generic;

namespace OnlineShop.RuleRepository
{
    public class Cart
    {
        List<Product> products = new List<Product>();
        public Cart()
        {
        }

        public decimal Total { get; internal set; }

        internal void Add(Product product)
        {
            products.Add(product);
        }

        public List<Product> Items
        {
            get { return products; }
        }
    }
}