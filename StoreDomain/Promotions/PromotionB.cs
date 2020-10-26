using System.Reflection;
using log4net;
using Repository.Interfaces;

namespace StoreDomain.Promotions
{
    /// GroupPromotionalPrice
    public class PromotionB : IPromotionCalculator
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private IPromotion _promotion;

        public decimal Calculate(int itemCount, decimal originalPrice)
        {
            log.Info("Applying the promotion: GroupPromotionalPrice");
            decimal price = originalPrice;
            if (itemCount >= _promotion.QuantityBought)
            {
                decimal groupPrice = _promotion.QuantityBought * _promotion.PromotionalPricing;
                int totalGroups = itemCount / _promotion.QuantityBought;
                int leftOver = itemCount % _promotion.QuantityBought;

                price = totalGroups * groupPrice + leftOver * originalPrice;
                
                return price;
            }

            return price * itemCount;            
        }

        public string GetAppliedPromotion()
        {
            return $"Buy Groups of {_promotion.QuantityBought} @ {_promotion.PromotionalPricing} ";
        }

        public PromotionB(IPromotion promotion)
        {
            _promotion = promotion;
        }
    }
}