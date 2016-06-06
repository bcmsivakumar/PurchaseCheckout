namespace OnlineShop.RuleRepository
{
    public class PurchaseRule : Rule, IRule
    {        
        public int Quantity { get;  set; }
        public decimal Amount { get;  set; }

        public PurchaseRule(string productCode, int quantity, decimal amount)
        {
            ProductCode = productCode;
            Quantity = quantity;
            Amount = amount;
        }
    }
}