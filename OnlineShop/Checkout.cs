using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.DataRepository;
using OnlineShop.RuleRepository;

namespace OnlineShop
{
    public delegate void ItemScannedEventHandler(object sender, ItemScannedEventHandlerArgs e);

    public class Checkout
    {
        public event ItemScannedEventHandler ItemScanned;

        Cart cart=new Cart();
        IPolicy policy;
        IProductService productService;
        
        public Checkout(IPolicy policy, IProductService productService)
        {
            if (policy == null)
                throw new ArgumentNullException("policy");
            if (productService == null)
                throw new ArgumentNullException("productService");

            this.policy = policy;
            this.productService = productService;
        }

        public void ScanItem(string code)
        {
            cart.Add(productService.GetProductByProductCode(code));
            policy.Apply(cart);
            if (ItemScanned != null)
                ItemScanned(this, new ItemScannedEventHandlerArgs(cart.Total));
        }

        public decimal Total
        {
            get {
                return cart.Total;
            }
        }
    }
}
