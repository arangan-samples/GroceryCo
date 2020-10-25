using System.Collections.Generic;
using Repository.Interfaces;
using StoreDomain.Interfaces;
using StoreDomain.Promotions;

namespace StoreDomain
{
    public class PromotionalPrice : IPromotionalPrice
    {
        private IDictionary<int,IPromotion> _promotions;
        public decimal Apply(KeyValuePair<int, int> cartItem, decimal originalPrice)
        {
            decimal price = originalPrice;
            if (_promotions.ContainsKey(cartItem.Key))
            {
                IPromotion promotion = _promotions[cartItem.Key];

                IPromotionCalculator currentPromotion = null;
                switch(promotion.PromotionCode)
                {
                    // case OfferedPromotions.AdditionalProductDiscount:
                    case var pcode when string.Equals(pcode, OfferedPromotions.AdditionalProductDiscount, System.StringComparison.OrdinalIgnoreCase):
                        currentPromotion = new PromotionA();
                        break;

                    // case OfferedPromotions.ReducedPrice.ToString():
                    case var pcode when string.Equals(pcode, OfferedPromotions.GroupPromotionalPrice, System.StringComparison.OrdinalIgnoreCase):
                    //currentPromotion = new PromotionB();
                    break;

                    default:
                        break;
                }
                if (null != currentPromotion)
                {
                    price = currentPromotion.Calculate(promotion, cartItem.Value, originalPrice);
                }
            }
            
            return cartItem.Value * price;
        }

        public PromotionalPrice(IStoreRepository storeRepository)
        {
            _promotions = storeRepository.GetPromotions();
        }
    }
}