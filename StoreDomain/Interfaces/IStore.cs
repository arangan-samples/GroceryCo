using System.Collections.Generic;
using Repository.Interfaces;
using StoreDomain.Interfaces;

public interface IStore
{
    ICart CreateCart();

    IDictionary<int, IProduct> Products {get;}

    IEnumerable<ICartItem> Scan(string[] items);

    void Checkout(ICart cart);
}
