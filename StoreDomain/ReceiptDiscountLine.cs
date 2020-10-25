using StoreDomain.Interfaces;

namespace StoreDomain
{
    public class ReceiptDiscountLine
    {
        public string Description {get;private set;}
        public decimal DiscountedPrice {get;private set;}

        public ReceiptDiscountLine(string description, decimal discountedPrice)
        {
            Description = description;
            DiscountedPrice = discountedPrice;
        }
    }
}