using System.Collections.Generic;

namespace StoreDomain.Interfaces
{
    public interface ISalePrice
    {
        decimal Apply(KeyValuePair<int,int> cartItem, decimal originalPrice);
    }
}