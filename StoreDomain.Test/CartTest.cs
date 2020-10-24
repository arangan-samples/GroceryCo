using Xunit;
using StoreDomain.Interfaces;

namespace StoreDomain.Test
{
    public class CartTest
    {
        private ICart _cart;
        public CartTest()
        {
            _cart = new Cart();
        }

        [Fact]
        public void Can_Add_Items_To_Cart()
        {
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
        public void Can_Scan_Cart_Items()
        {
            
        }
    }
}