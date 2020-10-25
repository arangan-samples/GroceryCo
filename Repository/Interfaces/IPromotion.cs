using System.Collections.Generic;

namespace Repository.Interfaces
{
    public interface IPromotion
    {
        string PromotionCode {get;}
        int PLU {get;}

        int QuantityBought {get;}

        int QuantityOffered {get;}

        decimal PromotionalPricing {get;}
    }
}