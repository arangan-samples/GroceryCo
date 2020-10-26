using StoreDomain.Interfaces;

namespace StoreDomain
{
    public class ReceiptLineItem
    {
        public int PLU {get;}

        public string Description {get;}

        public int Quantity {get;}

        public decimal ItemPrice {get;}

        public decimal FinalPrice 
        {
            get 
            {
                if (null != DiscountLine)
                {
                    return DiscountLine.DiscountedPrice;
                }
                
                return ItemPrice*Quantity;
            }
        }

        public ReceiptDiscountLine DiscountLine {get;private set;}

        public void SetDiscountLine(string description, decimal discountedPrice)
        {
            DiscountLine = new ReceiptDiscountLine(description, discountedPrice);
        }

        public ReceiptLineItem(int plu, string description, int quantity, decimal itemPrice)
        {
            PLU = plu;
            Description = description;
            Quantity = quantity;
            ItemPrice = itemPrice;
        }
    }
}