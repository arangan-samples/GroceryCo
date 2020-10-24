using System;
using Xunit;
using System.Collections.Generic;
using Repository.Interfaces;

namespace Repository.Test
{
    public class RepositoryTest
    {
        StoreRepository _repository;

        public RepositoryTest()
        {
            _repository = new StoreRepository();
        }

        #region ***** Products *****

        [Fact]
        public void Can_Get_ALL_Products()
        {
            Dictionary<int,IProduct> products = _repository.GetProducts();
            Assert.Equal(4, products.Count);

            Assert.Equal(2141,products[2141].PLU);
            Assert.Equal("Apple",products[2141].Name);
            Assert.Equal(3.00m, products[2141].Price, 2);

            Assert.Equal(2101,products[2101].PLU);
            Assert.Equal("Orange",products[2101].Name);
            Assert.Equal(2.00m, products[2101].Price, 2);

            Assert.Equal(3291,products[3291].PLU);
            Assert.Equal("Pineapple",products[3291].Name);
            Assert.Equal(4.55m, products[3291].Price, 2);

            
            Assert.Equal("Banana",products[4011].Name);
            Assert.Equal(4011,products[4011].PLU);
            Assert.Equal(1.00m, products[4011].Price, 2);
        }

        [Fact]
        public void Invalid_PLU_In_ProductList_Throws_Exception()
        {
            _repository.ProductsList =  "Products_InvalidPLU.txt";
            FormatException formatExeption = Assert.Throws<FormatException>(()=>{ _repository.GetProducts(); });
            Assert.Equal(typeof(FormatException), formatExeption.GetType());
        }

        [Fact]
        public void Blank_or_Missing_Name_Throws_Exception()
        {
            _repository.ProductsList =  "Products_InvalidName.txt";
            Exception formatExeption = Assert.Throws<Exception>(()=>{ _repository.GetProducts(); });
            Assert.Equal(typeof(Exception), formatExeption.GetType());
            Assert.Equal("Empty or Invalid Product Name", formatExeption.Message);
        }

        [Fact]
        public void Invalid_Price_Throws_Exception()
        {
            _repository.ProductsList =  "Products_InvalidPrice.txt";
            FormatException formatExeption = Assert.Throws<FormatException>(()=>{ _repository.GetProducts(); });
            Assert.Equal(typeof(FormatException), formatExeption.GetType());
        }

        [Fact]
        public void Invalid_Products_File_With_More_Fields_Throws_Error()
        {
            _repository.ProductsList =  "Products_InvalidFormat1.txt";
            Exception formatExeption = Assert.Throws<Exception>(()=>{ _repository.GetProducts(); });
            Assert.Equal(typeof(Exception), formatExeption.GetType());
            Assert.Equal("Error: Invalid Master Product List Format", formatExeption.Message);
        }

        [Fact]
        public void Invalid_Products_File_With_Less_Fields_Throws_Error()
        {
            _repository.ProductsList =  "Products_InvalidFormat2.txt";
            Exception formatExeption = Assert.Throws<Exception>(()=>{ _repository.GetProducts(); });
            Assert.Equal(typeof(Exception), formatExeption.GetType());
            Assert.Equal("Error: Invalid Master Product List Format", formatExeption.Message);
        }                
        
        #endregion ******************

        #region ***** Sale *****
        
        [Fact]
        public void Can_Get_ALL_Products_OnSale()
        {
            Dictionary<int,ISale> products = _repository.GetSalePrices();
            Assert.Equal(4, products.Count);

            Assert.Equal(2141,products[2141].PLU);
            Assert.Equal(2101,products[2101].PLU);
            Assert.Equal(3291,products[3291].PLU);
            Assert.Equal(4011,products[4011].PLU);
            
            Assert.Equal(0.99m, products[2141].Price, 2);
            Assert.Equal(1.99m, products[2101].Price, 2);
            Assert.Equal(2.24m, products[3291].Price, 2);
            Assert.Equal(0.77m, products[4011].Price, 2);
        }

        [Fact]
        public void Invalid_PLU_In_SalePriceList_Throws_Exception()
        {
            _repository.SalePriceList =  "SalePriceList_InvalidPLU.txt";
            FormatException formatExeption = Assert.Throws<FormatException>(()=>{ _repository.GetSalePrices(); });
            Assert.Equal(typeof(FormatException), formatExeption.GetType());
        }

        [Fact]
        public void Invalid_SalePrice_Throws_Exception()
        {
            _repository.SalePriceList =  "SalePriceList_InvalidPrice.txt";
            FormatException formatExeption = Assert.Throws<FormatException>(()=>{ _repository.GetSalePrices(); });
            Assert.Equal(typeof(FormatException), formatExeption.GetType());
        }

        [Fact]
        public void Invalid_SalePriceList_File_With_More_Fields_Throws_Error()
        {
            _repository.SalePriceList =  "SalePriceList_InvalidFormat1.txt";
            Exception formatExeption = Assert.Throws<Exception>(()=>{ _repository.GetSalePrices(); });
            Assert.Equal(typeof(Exception), formatExeption.GetType());
            Assert.Equal("Error: Invalid Sale List Format", formatExeption.Message);
        }

        [Fact]
        public void Invalid_SalePriceList_File_With_Less_Fields_Throws_Error()
        {
            _repository.SalePriceList =  "SalePriceList_InvalidFormat2.txt";
            Exception formatExeption = Assert.Throws<Exception>(()=>{ _repository.GetSalePrices(); });
            Assert.Equal(typeof(Exception), formatExeption.GetType());
            Assert.Equal("Error: Invalid Sale List Format", formatExeption.Message);
        }                
        
        #endregion **********

        #region ***** Promotions *****
        
        #endregion **********
    }
}
