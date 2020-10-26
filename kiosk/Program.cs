using System;
using System.IO;
using System.Linq;
using StoreDomain;
using StoreDomain.Interfaces;

namespace kiosk
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Specify Items file, which will be scanned");
            }
            string fileName = args[0];
            if (!File.Exists(fileName))
            {
                Console.WriteLine($"{fileName} -- NOT FOUND");
            }

            ICart cart = new Cart();
            cart.Scan(fileName);
            Receipt receipt = cart.Checkout();
            var lst = receipt.GetLineItems().ToList();
            foreach(var lineItem in receipt.GetLineItems())
            {
                Console.WriteLine($"{lineItem.PLU} - {lineItem.Description} - {lineItem.Quantity} @ {lineItem.ItemPrice} ");
                if (null != lineItem.DiscountLine)
                {
                    Console.WriteLine(lineItem.DiscountLine.Description);
                }
                Console.WriteLine();
            }
            Console.WriteLine(new String('-',20));
            Console.WriteLine($"GrandTotal - {receipt.GrandTotal}");

            if (null != receipt.IgnoredItems && receipt.IgnoredItems.Count > 0)
            {
                Console.WriteLine("\n\n");
                Console.WriteLine("Ignored Items");
                Console.WriteLine(new String('-',20));
                foreach (var item in receipt.IgnoredItems)
                {
                    Console.WriteLine($"{item.PLU} - {item.Description}");
                }
            }
        }
    }
}
