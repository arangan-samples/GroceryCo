using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace StoreDomain.Test.IntegrationTests
{
    public class CartTest
    {
        [Fact]
        public void Can_Check_Out_And_Get_A_Receipt()
        {
            Cart cart = new Cart();
            cart.Scan("CartItems.txt");
            Receipt receipt = cart.Checkout();

            List<ReceiptLineItem> receiptLineItems = receipt.GetLineItems().ToList();

            Assert.Equal(4, receiptLineItems.Count);
            Assert.Equal(1, receipt.IgnoredItems.Count);

            Assert.Equal(2141, receiptLineItems[0].PLU);
            Assert.Equal(5.97m, receiptLineItems[0].FinalPrice);

            Assert.Equal(2101, receiptLineItems[1].PLU);
            Assert.Equal(6.00m, receiptLineItems[1].FinalPrice);

            Assert.Equal(3291, receiptLineItems[2].PLU);
            Assert.Equal(6.60m, receiptLineItems[2].FinalPrice);

            Assert.Equal(4011, receiptLineItems[3].PLU);
            Assert.Equal(3.00m, receiptLineItems[3].FinalPrice);

            Assert.Equal(21.57m, receipt.GrandTotal);

        }
    }
}