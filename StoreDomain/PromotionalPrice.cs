using System.Collections.Generic;
using Repository.Interfaces;
using StoreDomain.Interfaces;
using StoreDomain.Promotions;

namespace StoreDomain
{
    public class PromotionalPrice : IPromotionalPrice
    {
        private IDictionary<int, IPromotion> _promotions;
        private IPromotionCalculator _currentPromotion;

        public decimal Apply(KeyValuePair<int, int> cartItem, decimal originalPrice)
        {
            decimal price = cartItem.Value * originalPrice;
            if (null != _promotions && _promotions.ContainsKey(cartItem.Key))
            {
                IPromotion promotion = _promotions[cartItem.Key];

                switch (promotion.PromotionCode)
                {
                    case var pcode when string.Equals(pcode, OfferedPromotions.AdditionalProductDiscount, System.StringComparison.OrdinalIgnoreCase):
                        _currentPromotion = new PromotionA(promotion);
                        break;

                    case var pcode when string.Equals(pcode, OfferedPromotions.GroupPromotionalPrice, System.StringComparison.OrdinalIgnoreCase):
                        _currentPromotion = new PromotionB(promotion);
                        break;

                    default:
                        break;
                }
                if (null != _currentPromotion)
                {
                    price = _currentPromotion.Calculate(cartItem.Value, originalPrice);
                }
            }

            return price;
        }

        public string GetAppliedPromotion(int plu)
        {
            if (null != _currentPromotion)
            {
                return _currentPromotion.GetAppliedPromotion();
            }
            return string.Empty;
        }

        public PromotionalPrice(IStoreRepository storeRepository)
        {
            _promotions = storeRepository.GetPromotions();
            _currentPromotion = null;
        }
    }
}