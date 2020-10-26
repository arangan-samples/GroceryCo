
using Repository.Interfaces;

namespace StoreDomain.Promotions
{
    /// AdditionalProductDiscount
    public class PromotionA : IPromotionCalculator
    {
        private IPromotion _promotion;
        public decimal Calculate(int itemCount, decimal originalPrice)
        {
            decimal price = originalPrice;
            if (itemCount > _promotion.QuantityBought)
            {
                int setSize = _promotion.QuantityBought + _promotion.QuantityOffered;
                int totalGroups = itemCount / setSize;
                int leftOver = itemCount % setSize;

                decimal discountedPrice = originalPrice - (_promotion.PromotionalPricing / 100m) * originalPrice;
                decimal groupPrice = _promotion.QuantityBought * originalPrice + _promotion.QuantityOffered * discountedPrice;

                price = groupPrice * totalGroups;
                if (leftOver > _promotion.QuantityBought)
                {
                    price = price + _promotion.QuantityBought * originalPrice + (leftOver - _promotion.QuantityBought) * discountedPrice;
                }
                else
                {
                    price = price + leftOver * originalPrice;
                }
                return price;
            }

            return price * itemCount;
        }

        public string GetAppliedPromotion()
        {
            return $"Buy {_promotion.QuantityBought} - Get {_promotion.QuantityOffered} @ {_promotion.PromotionalPricing} off ";
        }

        public PromotionA(IPromotion promotion)
        {
            _promotion = promotion;
        }
    }
}

