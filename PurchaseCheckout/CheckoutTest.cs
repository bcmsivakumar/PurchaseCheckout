using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineShop;
using OnlineShop.DataRepository;
using OnlineShop.RuleRepository;

namespace PurchaseCheckout
{
    [TestClass]
    public class CheckoutTest
    {
        [TestMethod]
        public void Checkout_Cart_Empty()
        {
            IPolicy policy = new PurchaseOfferPolicy();            
            policy.Add(new PurchaseRule("A", 3, 130M));

            IProductService productService = new ProductService();
            Checkout checkout = new Checkout(policy, productService);

            Assert.AreEqual(0, checkout.Total);
        }

        [TestMethod]
        public void Checkout_Cart_Item_A()
        {
            IPolicy policy = new PurchaseOfferPolicy();
            policy.Add(new PurchaseRule("A", 3, 130M));

            IProductService productService = new ProductService();
            Checkout checkout = new Checkout(policy, productService);

            checkout.ScanItem("A");

            Assert.AreEqual(50, checkout.Total);
        }

        [TestMethod]
        public void Checkout_Cart_Item_3A()
        {
            IPolicy policy = new PurchaseOfferPolicy();
            policy.Add(new PurchaseRule("A", 3, 130M));

            IProductService productService = new ProductService();
            Checkout checkout = new Checkout(policy, productService);

            checkout.ScanItem("A");
            checkout.ScanItem("A");
            checkout.ScanItem("A");

            Assert.AreEqual(130, checkout.Total);
        }

        [TestMethod]
        public void Checkout_Cart_Item_3AB()
        {
            IPolicy policy = new PurchaseOfferPolicy();
            IRule rule = new PurchaseRule("A", 3, 130M);
            policy.Add(rule);

            IProductService productService = new ProductService();
            Checkout checkout = new Checkout(policy, productService);

            checkout.ScanItem("A");
            checkout.ScanItem("B");
            checkout.ScanItem("A");
            checkout.ScanItem("A");

            Assert.AreEqual(160, checkout.Total);
        }

        [TestMethod]
        public void Checkout_Cart_Item_3A2B()
        {
            IPolicy policy = new PurchaseOfferPolicy();
            policy.AddRange(new[] { new PurchaseRule("A", 3, 130M), new PurchaseRule("B", 2, 45M) });           

            IProductService productService = new ProductService();
            Checkout checkout = new Checkout(policy, productService);

            checkout.ScanItem("A");
            checkout.ScanItem("B");
            checkout.ScanItem("A");
            checkout.ScanItem("A");
            checkout.ScanItem("B");

            Assert.AreEqual(175, checkout.Total);
        }

        [TestMethod]
        public void Checkout_Cart_Item_6A()
        {
            IPolicy policy = new PurchaseOfferPolicy();
            policy.AddRange(new[] { new PurchaseRule("A", 3, 130M), new PurchaseRule("B", 2, 45M) });

            IProductService productService = new ProductService();
            Checkout checkout = new Checkout(policy, productService);

            checkout.ScanItem("A");
            checkout.ScanItem("A");
            checkout.ScanItem("A");
            checkout.ScanItem("A");
            checkout.ScanItem("A");
            checkout.ScanItem("A");

            Assert.AreEqual(260, checkout.Total);
        }

        [TestMethod]
        public void Checkout_Cart_Item_6A_Discount20Percentage()
        {
            IPolicy purchasePolicy = new PurchaseOfferPolicy();
            purchasePolicy.AddRange(new[] { new PurchaseRule("A", 3, 130M), new PurchaseRule("B", 2, 45M) });
            var policy = new DiscountOfferPolicy(purchasePolicy);
            policy.Add(new DiscountRule() { DiscountPercentage = 20, DiscountAmount = 100, MinTotalAmount = 200 });
            IProductService productService = new ProductService();
            Checkout checkout = new Checkout(policy, productService);

            checkout.ScanItem("A");
            checkout.ScanItem("A");
            checkout.ScanItem("A");
            checkout.ScanItem("A");
            checkout.ScanItem("A");
            checkout.ScanItem("A");

            Assert.AreEqual(208, checkout.Total);
        }

        [TestMethod]
        public void Checkout_Cart_Item_6A_Discount100()
        {
            IPolicy purchasePolicy = new PurchaseOfferPolicy();
            purchasePolicy.AddRange(new[] { new PurchaseRule("A", 3, 130M), new PurchaseRule("B", 2, 45M) });
            var policy = new DiscountOfferPolicy(purchasePolicy);
            policy.Add(new DiscountRule() { DiscountAmount = 100, MinTotalAmount = 200 });
            IProductService productService = new ProductService();
            Checkout checkout = new Checkout(policy, productService);

            checkout.ScanItem("A");
            checkout.ScanItem("A");
            checkout.ScanItem("A");
            checkout.ScanItem("A");
            checkout.ScanItem("A");
            checkout.ScanItem("A");

            Assert.AreEqual(160, checkout.Total);
        }

        [TestMethod]
        public void Checkout_Cart_Item_6A_EachItemScanned()
        {
            IPolicy purchasePolicy = new PurchaseOfferPolicy();
            purchasePolicy.AddRange(new[] { new PurchaseRule("A", 3, 130M), new PurchaseRule("B", 2, 45M) });
            //var policy = new DiscountOfferPolicy(purchasePolicy);
            //policy.Add(new DiscountRule() { DiscountAmount = 100, MinTotalAmount = 200 });
            IProductService productService = new ProductService();
            Checkout checkout = new Checkout(purchasePolicy, productService);

            int assertCount = 1;
            int groupCount = 0;
            var groupAmount = 0;
            var product = productService.GetProductByProductCode("A");
            checkout.ItemScanned += (sender, e) => 
            {
                
                if (assertCount % 3 == 0)
                {
                    groupCount++;
                    groupAmount = 130 * groupCount;
                    Assert.AreEqual(groupAmount, checkout.Total);
                }
                else
                {
                    var amount = groupAmount + (product.Price * (assertCount % 3));
                    Assert.AreEqual(amount, e.TotalAmount);
                    
                }
                assertCount++;
            };

            checkout.ScanItem("A");
            checkout.ScanItem("A");
            checkout.ScanItem("A");
            checkout.ScanItem("A");
            checkout.ScanItem("A");
            checkout.ScanItem("A");

            Assert.AreEqual(260, checkout.Total);
        }

    }
}
