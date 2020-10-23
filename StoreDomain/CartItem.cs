using StoreDomain.Interfaces;

namespace StoreDomain
{
    public class CartItem : ICartItem
    {
        public int PLU {get;}
        public string Description {get;}

        public CartItem(int plu, string description)
        {
            PLU = plu;
            Description = description;
        }
    }
}