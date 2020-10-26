using System.Collections.Generic;
using Repository.Interfaces;
using System.IO;
using System;
using log4net;
using System.Reflection;

namespace Repository
{
    public class StoreRepository : IStoreRepository
    {
        public string ProductsList {get;set;}
        public string SalePriceList {get;set;}
        public string PromotionsList {get;set;}

        //TODO to catch any duplicates 
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
        public Dictionary<int, ISale> GetSalePrices()
        {
            string[] prods = File.ReadAllLines(SalePriceList);
            Dictionary<int,ISale> productsOnSale = new Dictionary<int, ISale>(prods.Length);
            foreach(string prod in prods)
            {
                string[] productRecord = prod.Split('|');
                if (productRecord.Length != 2)
                {
                    log.Fatal("Error: Invalid Sale List Format");
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
                    log.Fatal("Error: Invalid Master Product List Format");
                    throw new System.Exception("Error: Invalid Master Product List Format");
                }
                int plu = Convert.ToInt32(productRecord[0]);
                string name = productRecord[1].Trim();
                decimal price = Convert.ToDecimal(productRecord[2]);
                products.Add(plu, new Product(plu,name,price));
            }
            return products;
        }

        public Dictionary<int, IPromotion> GetPromotions()
        {
            string[] promotionRecords = File.ReadAllLines(PromotionsList);
            Dictionary<int, IPromotion> promotions = new Dictionary<int, IPromotion>(promotionRecords.Length);
            foreach (string promo in promotionRecords)
            {
                string[] record = promo.Split('|',StringSplitOptions.RemoveEmptyEntries);
                if (record.Length != 5)
                {
                    log.Fatal("Error: Invalid promotion list format");
                    throw new System.Exception("Error: Invalid promotion list format");
                }
                string name = record[0].Trim();
                int plu = Convert.ToInt32(record[1]);
                int quantityBought = Convert.ToInt32(record[2]);                
                int quantityOffered = Convert.ToInt32(record[3]);            
                decimal promotionalPricing = Convert.ToDecimal(record[4]);

                promotions.Add(plu, new Promotion(name, plu, quantityBought, quantityOffered, promotionalPricing));
            }

            return promotions;
        }

        public StoreRepository()
        {
            ProductsList = "Products.txt";
            SalePriceList = "SalePriceList.txt";
            PromotionsList = "Promotions.txt";
        }
    }
}