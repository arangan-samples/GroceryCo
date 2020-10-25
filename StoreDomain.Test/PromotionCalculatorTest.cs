using Xunit;
using StoreDomain.Interfaces;
using Repository;
using Moq;
using Repository.Interfaces;
using System.Collections.Generic;
using StoreDomain.Promotions;

namespace StoreDomain.Test
{
    public class PromotionCalculatorTest
    {

        [Theory]
        [InlineData(3,2,50,10,2,16)]
        [InlineData(2,1,100,10,2,14)]
        [InlineData(1,1,50,10,2,15)]   
        [InlineData(4,1,100,2,3,6)]  
        [InlineData(2,1,20,6,3,16.80)]  
        public void Can_Calculate_Promotional_Discount(int promotionQuantityWhenBought, 
                                                       int promotionQuantityOffered, 
                                                       int promotionalDiscountOffered,
                                                       int numberOfItemsBought,
                                                       decimal originalPrice,
                                                       decimal expectedPrice)
        {
            IPromotionCalculator promotionCalculator = new PromotionA();
            IPromotion promotion = new Promotion("TestPromotion",1000, promotionQuantityWhenBought, promotionQuantityOffered, promotionalDiscountOffered);
            decimal promotionalPrice = promotionCalculator.Calculate(promotion, numberOfItemsBought, originalPrice);

            Assert.Equal(expectedPrice, promotionalPrice);
        }
    }
}