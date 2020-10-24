using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Repository;
using Repository.Interfaces;
using StoreDomain.Interfaces;

[assembly: InternalsVisibleToAttribute("StoreDomain.Test")]
namespace StoreDomain
{
    internal class SalePrice : ISalePrice
    {
        private IDictionary<int, ISale> _salePrices;

        public decimal Apply(KeyValuePair<int, int> cartItem, decimal originalPrice)
        {
            decimal price = originalPrice;
            if (_salePrices.ContainsKey(cartItem.Key))
            {
                price = _salePrices[cartItem.Key].Price;
            }

            return cartItem.Value * price;
        }

        public SalePrice(IStoreRepository storeRepository)
        {
            _salePrices =  storeRepository.GetSalePrices();
        }
    }
}