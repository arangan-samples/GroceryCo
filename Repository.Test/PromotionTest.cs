using System;
using Repository.Interfaces;
using Xunit;

namespace Repository.Test
{
    public class PromotionTest
    {
        [Theory]
        [InlineData(" ")]
        [InlineData(null)]
        public void Invalid_PromotionCode_throws_error(string promotionCode)
        {
            Exception formatExeption = Assert.Throws<Exception>(()=>{ new Promotion(promotionCode,2011,10,5,30); });
        }
        
        [Fact]
        public void Correct_Parameters_Create_A_Promotion_Item()
        {
            IPromotion promotion = new Promotion("X2",2011,10,5,30);
            Assert.Equal("X2", promotion.PromotionCode);
            Assert.Equal(2011, promotion.PLU);
            Assert.Equal(10, promotion.QuantityBought);
            Assert.Equal(5, promotion.QuantityOffered);
            Assert.Equal(30, promotion.PromotionalPricing);
        }
    }
}