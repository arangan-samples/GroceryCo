
using System.Collections.Generic;
using Repository.Interfaces;

namespace Repository.Interfaces
{
    public interface IStoreRepository
    {
        Dictionary<int,IProduct> GetProducts();

        Dictionary<int,ISale> GetSalePrices();

        Dictionary<int, IPromotion> GetPromotions();
    }
}