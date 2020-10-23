using System;
using Repository;
using Repository.Interfaces;
using Xunit;
using StoreDomain.Interfaces;
using System.IO;
using System.Collections.Generic;

namespace StoreDomain.Test
{
    public class StoreTest
    {
        private IStore _store;
        private IStoreRepository _repository;
        private ICart _cart;

        public StoreTest()
        {
            _repository = new StoreRepository();
            _store = new Store(_repository);
            _cart = _store.CreateCart();
        }
        
        [Fact]
        public void Can_Scan_Cart_Items()
        {
            string[] cartItemsRecords = File.ReadAllLines("CartItems.txt");
            IEnumerable<ICartItem> cartItems = _store.Scan(cartItemsRecords);
            ICart cart = _store.CreateCart();

            foreach(ICartItem cartItem in cartItems)
            {
                cart.AddItem(cartItem);
            }

            _store.Checkout(cart);
            

            Assert.Equal(7,cartItemsRecords.Length);
            // _cart.AddItem(new Product(12, "Apple",))
        }
    }
}
