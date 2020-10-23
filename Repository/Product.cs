using System;
using Repository.Interfaces;

namespace Repository
{
    public class Product : IProduct
    {
        public int PLU { get;}
        public string Name { get;}
        public decimal Price { get;}

        public Product(int plu, string name, decimal price)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("Empty or Invalid Product Name");
            }

            PLU = plu;
            Name = name;
            Price = Decimal.Truncate(price*100)/100;
        }
    }
}