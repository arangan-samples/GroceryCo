using System.Collections.Generic;
using Repository.Interfaces;
using StoreDomain.Interfaces;

namespace StoreDomain
{
    public class Cart : ICart
    {
        public Dictionary<int, IList<ICartItem>> CartItems {get;}

        public void AddItem(ICartItem cartItem)
        {
            if (CartItems.ContainsKey(cartItem.PLU))
            {
                CartItems[cartItem.PLU].Add(cartItem);
            }
            else
            {
                IList<ICartItem> lst = new List<ICartItem>();
                lst.Add(cartItem);
                CartItems.Add(cartItem.PLU, lst);
            }
        }

        public Cart()
        {
            CartItems = new Dictionary<int, IList<ICartItem>>();
        }
    }
}