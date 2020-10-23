using System.Collections.Generic;
using Repository.Interfaces;
using System.IO;
using System;
using System.Linq;

namespace Repository
{
    public class StoreRepository : IStoreRepository
    {
        public string ProductsList {get;set;}
        public string SalePriceList {get;set;}
        public string PromotionsList {get;set;}

        public Dictionary<int, ISale> GetSalePrices()
        {
            string[] prods = File.ReadAllLines(SalePriceList);
            Dictionary<int,ISale> productsOnSale = new Dictionary<int, ISale>(prods.Length);
            foreach(string prod in prods)
            {
                string[] productRecord = prod.Split('|');
                if (productRecord.Length != 2)
                {
                    throw new System.Exception("Error: Invalid Sale List Format");
                }
                int plu = Convert.ToInt32(productRecord[0]);
                decimal price = Convert.ToDecimal(productRecord[1]);
                productsOnSale.Add(plu, new Sale(plu,price));
            }
            return productsOnSale;
        }

        public Dictionary<int,IProduct> GetProducts()
        {
            string[] prods = File.ReadAllLines(ProductsList);
            Dictionary<int,IProduct> products = new Dictionary<int, IProduct>(prods.Length);
            foreach(string prod in prods)
            {
                string[] productRecord = prod.Split('|');
                if (productRecord.Length != 3)
                {
                    throw new System.Exception("Error: Invalid Master Product List Format");
                }
                int plu = Convert.ToInt32(productRecord[0]);
                string name = productRecord[1];
                decimal price = Convert.ToDecimal(productRecord[2]);
                products.Add(plu, new Product(plu,name,price));
            }
            return products;
        }

        public Dictionary<int, IPromotion> GetPromotions()
        {
            throw new NotImplementedException();
        }

        public StoreRepository()
        {
            ProductsList = "Products.txt";
            SalePriceList = "SalePriceList.txt";
            PromotionsList = "Promotions.txt";
        }
    }
}