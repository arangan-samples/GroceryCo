using System.Collections.Generic;

namespace StoreDomain.Interfaces
{
    public interface IPromotion
    {
        decimal Apply(KeyValuePair<int,int> cartItem, decimal originalPrice);
    }
}