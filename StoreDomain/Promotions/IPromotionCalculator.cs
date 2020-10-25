using Repository.Interfaces;

namespace StoreDomain.Promotions
{
    public interface IPromotionCalculator
    {
        decimal Calculate(IPromotion promotion, int itemCount, decimal originalPrice);
    }
}