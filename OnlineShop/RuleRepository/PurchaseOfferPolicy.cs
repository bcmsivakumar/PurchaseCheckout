using System;
using OnlineShop;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.RuleRepository
{
    public class PurchaseOfferPolicy : OfferPolicy<PurchaseRule>,IPolicy
    {
        public override void Apply(Cart cart)
        {
            cart.Total = 0;            
            var distinctProducts = cart.Items.Select(x => x.ItemCode).Distinct().ToList();
            foreach (var productCode in distinctProducts)
            {
                var productRule = RuleCollection.Where(x => x.ProductCode == productCode).FirstOrDefault();
                var productPrice = cart.Items.Where(x => x.ItemCode == productCode)
                                            .Select(x => x.Price).First();
                var productListCount = cart.Items.Where(x => x.ItemCode == productCode).Count();

                if (productRule != null)
                {
                    var groupOfferCount = productListCount / productRule.Quantity;
                    var remainingCount = productListCount % productRule.Quantity;

                    cart.Total += groupOfferCount * productRule.Amount;
                    cart.Total += remainingCount * productPrice;
                }
                else
                {
                    cart.Total += productPrice * productListCount;
                }

            }
        }
    }
}