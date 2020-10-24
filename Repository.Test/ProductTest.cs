using System;
using Repository.Interfaces;
using Xunit;

namespace Repository.Test
{
    public class ProductTest
    {
        [Theory]
        [InlineData(" ")]
        [InlineData(null)]
        public void Invalid_Product_Name_throw_error(string productName)
        {
            Exception formatExeption = Assert.Throws<Exception>(()=>{ new Product(1,productName,10m); });
        }

        [Fact]
        public void Correct_Parameters_Create_A_Product()
        {
            IProduct product = new Product(1,"productName",10m);
            Assert.Equal(1, product.PLU);
            Assert.Equal("productName", product.Name);
            Assert.Equal(10m, product.Price);
        }
    }
}