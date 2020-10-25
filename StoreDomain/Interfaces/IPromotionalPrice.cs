using System.Collections.Generic;

namespace StoreDomain.Interfaces
{
    public interface IPromotionalPrice
    {
        string GetAppliedPromotion(int plu);
        decimal Apply(KeyValuePair<int,int> cartItem, decimal originalPrice);
    }
}