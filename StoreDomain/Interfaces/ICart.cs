using System.Collections.Generic;
using Repository.Interfaces;

namespace StoreDomain.Interfaces 
{
    public interface ICart 
    {
        Dictionary<int,int> CartItems {get;}
        void AddItem(ICartItem product);
        Receipt Checkout();

        void Scan(string fileName);
    }
}