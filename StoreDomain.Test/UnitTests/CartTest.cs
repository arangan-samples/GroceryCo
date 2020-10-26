using Xunit;
using StoreDomain.Interfaces;
using Moq;
using Repository;
using System.Collections.Generic;
using Repository.Interfaces;
using System.Linq;

namespace StoreDomain.Test.UnitTests
{
    public class CartTest
    {
        private ICart _cart;
        Mock<IStoreRepository> _storeRepository;

        public CartTest()
        {
            _storeRepository = new Mock<IStoreRepository>();
            Dictionary<int, IProduct> products = new Dictionary<int, IProduct>(3);
            products.Add(2141, new Product(2141, "Apple", 3.12m));
            products.Add(2101, new Product(2101, "Orange", 2.49m));
            products.Add(3291, new Product(3291, "Pineapple", 4.5595m));
            products.Add(4011, new Product(4011, "Banana", 0.99m));
            _storeRepository.Setup(fn => fn.GetProducts()).Returns(products);
        }

        [Fact]
        public void Can_Scan_Items_From_A_File()
        {
            ICart cart = new Cart();
            cart.Scan("CartItems_Test.txt");
            Assert.Equal(3, cart.CartItems.Count);
            Assert.True(cart.CartItems.ContainsKey(2001));
            Assert.True(cart.CartItems.ContainsKey(3001));
            Assert.True(cart.CartItems.ContainsKey(4222)); 
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
            _cart.AddItem(new CartItem(2141, "Apple"));
            _cart.AddItem(new CartItem(3001, "Grapes"));
            _cart.AddItem(new CartItem(4011, "Banana"));

            Receipt receipt = _cart.Checkout();
            List<ReceiptLineItem> receiptLineItems = receipt.GetLineItems().ToList();

            Assert.Equal(2, receiptLineItems.Count);
            Assert.Equal(1, receipt.IgnoredItems.Count);

            Assert.Equal(2141, receiptLineItems[0].PLU);
            Assert.Equal(1.99m, receiptLineItems[0].FinalPrice);

            Assert.Equal(4011, receiptLineItems[1].PLU);
            Assert.Equal(0.89m, receiptLineItems[1].FinalPrice);
            Assert.Equal(2.88m, receipt.GrandTotal);
        }

        [Fact]
        public void Can_Checkout_And_Get_Best_Promotion()
        {
            Dictionary<int, IPromotion> promotions = new Dictionary<int, IPromotion>(2);
            promotions.Add(2141, new Promotion("GroupPromotionalPrice", 2141, 4, 0, 2.12m));
            promotions.Add(2101, new Promotion("AdditionalProductDiscount", 2101, 3, 1, 100));
            _storeRepository.Setup(fn => fn.GetPromotions()).Returns(promotions);

            _cart = new Cart(_storeRepository.Object);
            _cart.AddItem(new CartItem(2141, "Apple"));
            _cart.AddItem(new CartItem(2141, "Apple"));
            _cart.AddItem(new CartItem(2141, "Apple"));
            _cart.AddItem(new CartItem(2141, "Apple"));
            _cart.AddItem(new CartItem(2141, "Apple"));

            _cart.AddItem(new CartItem(2101, "Orange"));
            _cart.AddItem(new CartItem(2101, "Orange"));
            _cart.AddItem(new CartItem(2101, "Orange"));
            _cart.AddItem(new CartItem(2101, "Orange"));
            _cart.AddItem(new CartItem(2101, "Orange"));
            _cart.AddItem(new CartItem(2101, "Orange"));

            _cart.AddItem(new CartItem(3001, "Grapes"));

            _cart.AddItem(new CartItem(4011, "Banana"));
            _cart.AddItem(new CartItem(4011, "Banana"));

            Receipt receipt = _cart.Checkout();
            List<ReceiptLineItem> receiptLineItems = receipt.GetLineItems().ToList();

            Assert.Equal(3, receiptLineItems.Count);
            Assert.Equal(1, receipt.IgnoredItems.Count);

            Assert.Equal(2141, receiptLineItems[0].PLU);
            Assert.Equal(11.60m, receiptLineItems[0].FinalPrice);

            Assert.Equal(2101, receiptLineItems[1].PLU);
            Assert.Equal(12.45m, receiptLineItems[1].FinalPrice);

            Assert.Equal(4011, receiptLineItems[2].PLU);
            Assert.Equal(1.98m, receiptLineItems[2].FinalPrice);

            Assert.Equal(26.03m, receipt.GrandTotal);
        }

        [Fact]
        public void Can_Checkout_And_Get_Best_Sale_Price_Or_Promotion()
        {
            Dictionary<int, ISale> discounts = new Dictionary<int, ISale>(2);
            discounts.Add(2141, new Sale(2141, 1.56m));
            discounts.Add(4011, new Sale(4011, 0.89m));
            _storeRepository.Setup(foo => foo.GetSalePrices()).Returns(discounts);

            Dictionary<int, IPromotion> promotions = new Dictionary<int, IPromotion>(2);
            promotions.Add(2141, new Promotion("GroupPromotionalPrice", 2141, 4, 0, 1.56m));
            promotions.Add(4011, new Promotion("AdditionalProductDiscount", 4011, 3, 1, 100));
            _storeRepository.Setup(fn => fn.GetPromotions()).Returns(promotions);

            _cart = new Cart(_storeRepository.Object);
            _cart.AddItem(new CartItem(2141, "Apple"));
            _cart.AddItem(new CartItem(2141, "Apple"));
            _cart.AddItem(new CartItem(2141, "Apple"));

            _cart.AddItem(new CartItem(4011, "Banana"));
            _cart.AddItem(new CartItem(4011, "Banana"));
            _cart.AddItem(new CartItem(4011, "Banana"));
            _cart.AddItem(new CartItem(4011, "Banana"));    

            _cart.AddItem(new CartItem(3001, "Grapes"));
            _cart.AddItem(new CartItem(3291, "Pineapple"));            
            _cart.AddItem(new CartItem(2101, "Orange"));


            Receipt receipt = _cart.Checkout();
            List<ReceiptLineItem> receiptLineItems = receipt.GetLineItems().ToList();

            Assert.Equal(4, receiptLineItems.Count);
            Assert.Equal(1, receipt.IgnoredItems.Count);

            Assert.Equal(2141, receiptLineItems[0].PLU);
            Assert.Equal(4.68m, receiptLineItems[0].FinalPrice);

            Assert.Equal(4011, receiptLineItems[1].PLU);
            Assert.Equal(2.97m, receiptLineItems[1].FinalPrice);

            Assert.Equal(3291, receiptLineItems[2].PLU);
            Assert.Equal(4.55m, receiptLineItems[2].FinalPrice);

            Assert.Equal(2101, receiptLineItems[3].PLU);
            Assert.Equal(2.49m, receiptLineItems[3].FinalPrice);

            Assert.Equal(14.69m, receipt.GrandTotal);
        }
    }
}