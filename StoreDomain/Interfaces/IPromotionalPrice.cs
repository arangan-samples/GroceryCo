using System.Collections.Generic;

namespace StoreDomain.Interfaces
{
    public interface IPromotionalPrice
    {
        decimal Apply(KeyValuePair<int,int> cartItem, decimal originalPrice);
    }
}