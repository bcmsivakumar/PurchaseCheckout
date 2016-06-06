using System;
using OnlineShop;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.RuleRepository
{
    public class DiscountOfferPolicy : OfferPolicy<DiscountRule>
    {
        private IPolicy policy;

        public DiscountOfferPolicy(IPolicy policy)
        {
            this.policy = policy;
        }

        public override void Apply(Cart cart)
        {
            if (policy != null)
                policy.Apply(cart);

            var discount = 0M;
            foreach (var rule in RuleCollection)
                discount += rule.GetDiscount(cart);

            cart.Total = cart.Total - discount;
        }
    }
}