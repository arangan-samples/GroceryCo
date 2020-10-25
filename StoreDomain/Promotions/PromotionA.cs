
using Repository.Interfaces;

namespace StoreDomain.Promotions
{
    public class PromotionA : IPromotionCalculator
    {
        public decimal Calculate(IPromotion promotion, int itemCount, decimal originalPrice)
        {
            decimal price = originalPrice;
            if (itemCount > promotion.QuantityBought)
            {
                int setSize = promotion.QuantityBought + promotion.QuantityOffered;
                int totalGroups = itemCount / setSize;
                int leftOver = itemCount % setSize;

                decimal discountedPrice = originalPrice - (promotion.PromotionalPricing / 100m) * originalPrice;
                decimal groupPrice = promotion.QuantityBought * originalPrice + promotion.QuantityOffered * discountedPrice;

                price = groupPrice * totalGroups;
                if (leftOver > promotion.QuantityBought)
                {
                    price = price + promotion.QuantityBought * originalPrice + (leftOver - promotion.QuantityBought) * discountedPrice;
                }
                else
                {
                    price = price + leftOver * originalPrice;
                }
                return price;
            }

            return price * itemCount;
        }
    }
}

