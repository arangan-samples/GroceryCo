using Xunit;
using StoreDomain.Interfaces;
using Repository;
using Moq;
using Repository.Interfaces;
using System.Collections.Generic;

namespace StoreDomain.Test.UnitTests
{
    public class SalePriceTest
    {
        Mock<IStoreRepository> _storeRepository;
        private ISalePrice _salePrice;

        [Fact]
        public void Can_Apply_Discount()
        {
            _storeRepository = new Mock<IStoreRepository>();
            Dictionary<int, ISale> discounts = new Dictionary<int, ISale>(2);
            discounts.Add(2141, new Sale(2141, 0.99m));
            discounts.Add(4011, new Sale(4011, 0.99m));
            _storeRepository.Setup(foo => foo.GetSalePrices()).Returns(discounts);

            _salePrice = new SalePrice(_storeRepository.Object);
            KeyValuePair<int, int> cartItem1 = new KeyValuePair<int, int>(2141, 3);
            decimal finalPrice = _salePrice.Apply(cartItem1, 3);

            Assert.Equal(2.97m, finalPrice, 2);
        }

        [Fact]
        public void Discount_Is_Not_Applied_If_Product_Is_Not_currently_on_Sale()
        {
            _storeRepository = new Mock<IStoreRepository>();
            Dictionary<int, ISale> discounts = new Dictionary<int, ISale>(3);
            discounts.Add(2141, new Sale(2141, 0.99m));
            discounts.Add(4011, new Sale(4011, 0.99m));
            _storeRepository.Setup(foo => foo.GetSalePrices()).Returns(discounts);

            _salePrice = new SalePrice(_storeRepository.Object);
            KeyValuePair<int, int> cartItem1 = new KeyValuePair<int, int>(2101, 3);
            decimal finalPrice = _salePrice.Apply(cartItem1, 2);

            Assert.Equal(6, finalPrice, 2);
        }

    }
}