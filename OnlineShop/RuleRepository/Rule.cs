using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.RuleRepository
{
    public class Rule : IRule
    {
        public string ProductCode
        {
            get; set;
        }
    }
}
