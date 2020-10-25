using System.Collections.Generic;

namespace StoreDomain.Interfaces
{
    public interface ISalePrice
    {
        public decimal? GetSalePrice(int plu);
        decimal Apply(KeyValuePair<int,int> cartItem, decimal originalPrice);
    }
}