using System;
using System.Collections.Generic;
using Repository;
using Repository.Interfaces;
using StoreDomain.Interfaces;

namespace StoreDomain
{
    public class Store : IStore
    {
        private IStoreRepository _storeRepository;

        public IDictionary<int, IProduct> Products { get; private set; }

        public void Checkout(ICart cart)
        {
            EndTransaction();
        }

        public ICart CreateCart()
        {
            return new Cart();
        }

        public IEnumerable<ICartItem> Scan(string[] items)
        {
            foreach (string item in items)
            {
                string[] itemRecord = item.Split('|', StringSplitOptions.RemoveEmptyEntries);
                if (itemRecord.Length == 2)
                {
                    int plu = -1;
                    if (!int.TryParse(itemRecord[0], out plu))
                    {
                        //Log this -- Invalid PLU
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(itemRecord[1]))
                    {
                        //Log this -- CartItem: Missing Description
                        continue;
                    }

                    if (!Products.ContainsKey(plu))
                    {
                        //Log this -- Product not found
                        continue;
                    }

                    yield return new CartItem(plu, itemRecord[1]);
                }
            }
        }

        internal bool BeginTransaction()
        {
            try
            {
                Products = _storeRepository.GetProducts();
                return true;
            }
            catch
            {
                //LOG this.
            }
            return false;
        }

        internal void EndTransaction()
        {
            Products = null;
        }

        public Store(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
            BeginTransaction();
        }
    }
}
