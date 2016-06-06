using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.RuleRepository
{
    public class DiscountRule: Rule, IRule
    {
        public int DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal MinTotalAmount { get; set; }

        public virtual decimal GetDiscount(Cart cart)
        {
            decimal discount = 0M;
            if ( cart.Total >= MinTotalAmount)
            {
                if (DiscountPercentage > 0)
                    discount=(cart.Total * DiscountPercentage / 100);                
                else
                    discount= DiscountAmount;
            }
            return discount;
        }
    }
}