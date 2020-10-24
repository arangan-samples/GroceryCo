using System.Collections.Generic;
using Repository;
using Repository.Interfaces;
using StoreDomain.Interfaces;

namespace StoreDomain
{
    public class Cart : ICart
    {
        private IStoreRepository _storeRepository;

        public Dictionary<int, int> CartItems { get; }

        public void AddItem(ICartItem cartItem)
        {
            if (CartItems.ContainsKey(cartItem.PLU))
            {
                CartItems[cartItem.PLU] = CartItems[cartItem.PLU] + 1;
            }
            else
            {
                CartItems.Add(cartItem.PLU, 1);
            }
        }

        public decimal ApplySalePrice(KeyValuePair<int, int> item, IDictionary<int, IProduct> products, IDictionary<int, ISale> salePrices)
        {
            decimal price = products[item.Key].Price;
            if (salePrices.ContainsKey(item.Key))
            {
                price = salePrices[item.Key].Price;
            }

            return item.Value * price;
        }

        public void Checkout()
        {
            IDictionary<int, IProduct> products = _storeRepository.GetProducts();
            IDictionary<int, ISale> salePrices = _storeRepository.GetSalePrices();

            foreach (KeyValuePair<int, int> cartItem in CartItems)
            {
                decimal price = ApplySalePrice(cartItem, products, salePrices);

                // foreach(Promotion p in Promotions)
                // {
                //     p.APply(PLU);
                // }
                // decimal price1 =  ApplyPromotion();


            }

        }

        public Cart()
        {
            CartItems = new Dictionary<int, int>();
            _storeRepository = new StoreRepository();
        }
    }
}