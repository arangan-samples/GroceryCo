using System;
using Repository.Interfaces;

namespace Repository
{
    public class Promotion : IPromotion
    {
        public string PromotionCode { get; }

        public int PLU { get; }

        public int QuantityBought { get; }

        public int QuantityOffered { get; }

        public int PromotionalPricing { get; }

        public Promotion(string promotionCode,
                         int plu,
                         int quantityBought,
                         int quantityOffered,
                         int promotionalPricing)
        {
            if (string.IsNullOrWhiteSpace(promotionCode))
            {
                throw new Exception("Empty or Invalid Promotion Code");
            }

            PromotionCode = promotionCode;
            PLU = plu;
            QuantityBought = quantityBought;
            QuantityOffered = quantityOffered;
            PromotionalPricing = promotionalPricing;
        }
    }
}