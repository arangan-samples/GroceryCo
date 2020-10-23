using System.Collections.Generic;
using Repository.Interfaces;

namespace StoreDomain.Interfaces 
{
    public interface ICart 
    {
        Dictionary<int,IList<ICartItem>> CartItems {get;}
        void AddItem(ICartItem product);
    }
}