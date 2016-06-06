
using System.Collections.Generic;

namespace OnlineShop.RuleRepository
{
    public interface IPolicy<T> : IPolicy
    {
        void Add(T rule);
        void Remove(T rule);
        void AddRange(IEnumerable<T> rule);
    }
}