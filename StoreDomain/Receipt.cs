using System.Collections.Generic;
using Repository.Interfaces;
using StoreDomain.Interfaces;

namespace StoreDomain
{
    public class Receipt
    {
        private IList<ReceiptLineItem> LineItems {get; }

        public IList<ICartItem> IgnoredItems {get; private set;}

        public IEnumerable<ReceiptLineItem> GetLineItems()
        {
            foreach(ReceiptLineItem item in LineItems)
            {
                yield return item;
            }
        }

        public decimal GrandTotal {get;set;}

        public void AddLineItem(ReceiptLineItem receiptLineItem)
        {
            LineItems.Add(receiptLineItem);
        }
        // public void AddLineItem(int plu, string description, int quantity, decimal finalPrice)
        // {
        //     LineItems.Add(new ReceiptLineItem(plu, description, quantity, finalPrice));
        // }

        public Receipt()
        {
            LineItems = new List<ReceiptLineItem>();
            IgnoredItems = new List<ICartItem>();          
        }
    }
}