using System;
using Repository.Interfaces;

namespace Repository
{
    public class Sale : ISale
    {
        public int PLU { get; }

        public decimal Price { get; }

        public Sale(int plu, decimal price)
        {
            PLU = plu;
            Price = Decimal.Truncate(price*100)/100;
        }
    }
}