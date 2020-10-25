using Xunit;
using StoreDomain.Interfaces;
using Moq;
using Repository;
using System.Collections.Generic;
using Repository.Interfaces;
using System.Linq;

namespace StoreDomain.Test
{
    public class CartTest
    {
        private ICart _cart;
        Mock<IStoreRepository> _storeRepository;

        public CartTest()
        {
            _storeRepository = new Mock<IStoreRepository>();
            Dictionary<int,IProduct> products = new Dictionary<int, IProduct>(3);
            products.Add(2141, new Product(2141,"Apple",3.12m));
            products.Add(2101, new Product(2101,"Orange",2.49m));
            products.Add(3291, new Product(3291,"Pineapple",4.5595m));
            products.Add(4011, new Product(4011,"Banana",0.99m));
            _storeRepository.Setup( fn => fn.GetProducts()).Returns(products);
        }

        [Fact]
        public void Can_Add_Items_To_Cart()
        {
            _cart = new Cart(_storeRepository.Object);

            ICartItem cartItem1 = new CartItem(2001, "Indus");
            ICartItem cartItem2 = new CartItem(3001, "Narmada");
            ICartItem cartItem3 = new CartItem(4222, "Saraswati");
            ICartItem cartItem4 = new CartItem(2001, "Indus");
            ICartItem cartItem5 = new CartItem(3001, "Narmada");

            _cart.AddItem(cartItem1);
            _cart.AddItem(cartItem2);
            _cart.AddItem(cartItem3);
            _cart.AddItem(cartItem4);
            _cart.AddItem(cartItem5);

            Assert.Equal(3, _cart.CartItems.Count);
            Assert.Equal(2, _cart.CartItems[2001]);
            Assert.Equal(2, _cart.CartItems[3001]);
            Assert.Equal(1, _cart.CartItems[4222]);
        }

        [Fact]
        public void Can_Checkout_And_Get_Best_Sale_Price()
        {
            Dictionary<int, ISale> discounts = new Dictionary<int, ISale>(2);
            discounts.Add(2141, new Sale(2141, 1.99m));
            discounts.Add(4011, new Sale(4011, 0.89m));
            _storeRepository.Setup(foo => foo.GetSalePrices()).Returns(discounts);

            _cart = new Cart(_storeRepository.Object);
            ICartItem cartItem1 = new CartItem(2141, "Apple");
            ICartItem cartItem2 = new CartItem(3001, "Grapes");
            ICartItem cartItem3 = new CartItem(4011, "Banana");
            _cart.AddItem(cartItem1);
            _cart.AddItem(cartItem2);
            _cart.AddItem(cartItem3);

            Receipt receipt = _cart.Checkout();
            List<ReceiptLineItem> receiptLineItems = receipt.GetLineItems().ToList();

            Assert.Equal(2, receiptLineItems.Count);
            Assert.Equal(1, receipt.IgnoredItems.Count);

            Assert.Equal(1.99m, receiptLineItems[0].FinalPrice);
            Assert.Equal(0.89m, receiptLineItems[1].FinalPrice);
            Assert.Equal(2.88m, receipt.GrandTotal);
        }
    }
}