using Xunit;
using Repository;
using Repository.Interfaces;
using StoreDomain.Promotions;

namespace StoreDomain.Test
{
    public class PromotionCalculatorTest
    {

        [Theory]
        [InlineData(3, 2, 50, 10, 2, 16)]
        [InlineData(2, 1, 100, 10, 2, 14)]
        [InlineData(1, 1, 50, 10, 2, 15)]
        [InlineData(4, 1, 100, 2, 3, 6)]
        [InlineData(2, 1, 20, 6, 3, 16.80)]
        public void Can_Calculate_Promotional_Discount(int promotionQuantityWhenBought,
                                                       int promotionQuantityOffered,
                                                       int promotionalDiscountOffered,
                                                       int numberOfItemsBought,
                                                       decimal originalPrice,
                                                       decimal expectedPrice)
        {

            IPromotion promotion = new Promotion("TestPromotion", 1000, promotionQuantityWhenBought, promotionQuantityOffered, promotionalDiscountOffered);
            IPromotionCalculator promotionCalculator = new PromotionA(promotion);
            decimal promotionalPrice = promotionCalculator.Calculate(numberOfItemsBought, originalPrice);
            string appliedPromotion = promotionCalculator.GetAppliedPromotion();
            Assert.Equal(expectedPrice, promotionalPrice);
            Assert.Equal($"Buy {promotionQuantityWhenBought} - Get {promotionQuantityOffered} @ {promotionalDiscountOffered} off ", appliedPromotion, true);
        }


        [Theory]
        [InlineData(3, 1, 10, 2, 11)]
        [InlineData(5, 1, 4, 2, 8)]
        [InlineData(4, 2, 4, 3, 8)]
        [InlineData(2, 0.5, 0, 1, 0)]
        public void Can_Calculate_Promotional_Group_Discount(int promotionQuantityWhenBought,
                                                             int promotionalDiscountOffered,
                                                             int numberOfItemsBought,
                                                             decimal originalPrice,
                                                             decimal expectedPrice)
        {
            IPromotion promotion = new Promotion("TestPromotion", 1000, promotionQuantityWhenBought, 0, promotionalDiscountOffered);
            IPromotionCalculator promotionCalculator = new PromotionB(promotion);
            decimal promotionalPrice = promotionCalculator.Calculate(numberOfItemsBought, originalPrice);
            string appliedPromotion = promotionCalculator.GetAppliedPromotion();
            Assert.Equal(expectedPrice, promotionalPrice);
            Assert.Equal($"Buy Groups of {promotionQuantityWhenBought} @ {promotionalDiscountOffered} ", appliedPromotion, true);
        }
    }
}