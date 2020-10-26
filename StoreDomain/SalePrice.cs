using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using log4net;
using Repository;
using Repository.Interfaces;
using StoreDomain.Interfaces;

[assembly: InternalsVisibleToAttribute("StoreDomain.Test")]
namespace StoreDomain
{
    internal class SalePrice : ISalePrice
    {
        private IDictionary<int, ISale> _salePrices;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public decimal? GetSalePrice(int plu)
        {
            if (_salePrices.ContainsKey(plu))
            {
                return _salePrices[plu].Price;
            }

            return null;
        }
        public decimal Apply(KeyValuePair<int, int> cartItem, decimal originalPrice)
        {
            log.Info("Checking Discount");
            decimal price = originalPrice;
            if (null != _salePrices && _salePrices.ContainsKey(cartItem.Key))
            {
                log.Info("Applying Discount");
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