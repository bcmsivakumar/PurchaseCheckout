
using System.Collections.Generic;

namespace OnlineShop.RuleRepository
{
    public interface IPolicy
    {
        void Apply(Cart cart);
        void Add(IRule purchaseRule);
        void AddRange(IEnumerable<IRule> purchaseRule);
    }
}