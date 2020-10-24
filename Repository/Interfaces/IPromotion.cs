namespace Repository.Interfaces
{
    public interface IPromotion
    {
        string PromotionCode {get;}
        int PLU {get;}

        int QuantityBought {get;}

        int QuantityOffered {get;}

        int PromotionalPricing {get;}
    }
}