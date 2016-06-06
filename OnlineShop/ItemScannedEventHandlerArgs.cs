using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
    public class ItemScannedEventHandlerArgs : EventArgs
    {
        private decimal totalAmount;

        public ItemScannedEventHandlerArgs(decimal totalAmount)
        {
            this.totalAmount = totalAmount;
        }

        public decimal TotalAmount { get { return totalAmount; } }
    }
}
