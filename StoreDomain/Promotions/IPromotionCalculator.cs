using Repository.Interfaces;

namespace StoreDomain.Promotions
{
    public interface IPromotionCalculator
    {
        string GetAppliedPromotion();
        decimal Calculate(int itemCount, decimal originalPrice);
    }
}