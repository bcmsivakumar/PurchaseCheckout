using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.RuleRepository
{
    public abstract class OfferPolicy<T> : IPolicy<T> where T : IRule
    {
        List<T> _ruleCollection = new List<T>();

        protected List<T> RuleCollection
        {
            get
            {
                return _ruleCollection;
            }
        }

        public void Add(T rule)
        {
            _ruleCollection.Add(rule);
        }

        public void AddRange(IEnumerable<T> rules)
        {
            _ruleCollection.AddRange(rules);
        }

        public void Remove(T rule)
        {
            var existRule = _ruleCollection.Find(x => x.ProductCode == rule.ProductCode);
            _ruleCollection.Remove(existRule);
        }

        public abstract void Apply(Cart cart);

        public void Add(IRule rule)
        {
            _ruleCollection.Add((T)rule);
        }

        public void AddRange(IEnumerable<IRule> rules)
        {
            _ruleCollection.AddRange((IEnumerable<T>)rules);
        }
    }
}
