using Repository.Interfaces;
using Xunit;

namespace Repository.Test
{
    public class SaleTest
    {
        [Fact]
        public void Correct_Parameters_Create_A_Sale_Item()
        {
            ISale sale = new Sale(2,10m);
            Assert.Equal(2, sale.PLU);
            Assert.Equal(10m, sale.Price);
        }
    }
}