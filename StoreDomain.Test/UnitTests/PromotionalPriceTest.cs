using System;
using System.Collections.Generic;
using Moq;
using Repository;
using Repository.Interfaces;
using StoreDomain.Interfaces;
using Xunit;

namespace StoreDomain.Test.UnitTests
{
    public class DataContext : IDisposable
    {
        public Mock<IStoreRepository> StoreRepository {get; private set;}

        public DataContext()
        {
            StoreRepository = new Mock<IStoreRepository>();
            Dictionary<int, IPromotion> promotions = new Dictionary<int, IPromotion>(2);
            promotions.Add(2500, new Promotion("GroupPromotionalPrice",2500,4,0,3));
            promotions.Add(2700, new Promotion("AdditionalProductDiscount",2700,3,1,50));
            StoreRepository.Setup(fn=>fn.GetPromotions()).Returns(promotions);            
        }

        public void Dispose()
        {
            StoreRepository = null;
        }
    }
    
    public class PromotionalPriceTest : IClassFixture<DataContext>
    {
        private DataContext _dataContext;
        private IPromotionalPrice _promotionalPrice;

        public PromotionalPriceTest(DataContext dataRepository)
        {
            _dataContext = dataRepository;
        }

        [Theory]
        [InlineData(2500, 6, 5, 22)]
        [InlineData(2700, 8, 6, 42)]
        public void correct_promotion_gets_instantiated(int plu, int itemCount, decimal originalPrice, decimal expectedPrice)
        {
            _promotionalPrice = new PromotionalPrice(_dataContext.StoreRepository.Object);

            KeyValuePair<int, int> cartItem1 = new KeyValuePair<int, int>(plu, itemCount);

            decimal finalPrice = _promotionalPrice.Apply(cartItem1, originalPrice);
            Assert.Equal(expectedPrice, finalPrice);
        }

    }
}