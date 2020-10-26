using System.Collections.Generic;
using Repository;
using Repository.Interfaces;
using StoreDomain.Interfaces;
using System.IO;
using System;

namespace StoreDomain
{
    public class Cart : ICart
    {
        private IStoreRepository _storeRepository;

        public Dictionary<int, int> CartItems { get; }

        private Dictionary<int, ICartItem> _cartItemDescription;

        public void AddItem(ICartItem cartItem)
        {
            if (CartItems.ContainsKey(cartItem.PLU))
            {
                CartItems[cartItem.PLU] = CartItems[cartItem.PLU] + 1;
            }
            else
            {
                CartItems.Add(cartItem.PLU, 1);
            }

            if (!_cartItemDescription.ContainsKey(cartItem.PLU))
            {
                _cartItemDescription.Add(cartItem.PLU, cartItem);
            }
        }

        public Receipt Checkout()
        {
            IDictionary<int, IProduct> products = _storeRepository.GetProducts();
            ISalePrice salePrice = new SalePrice(_storeRepository);
            IPromotionalPrice promotionalPrice = new PromotionalPrice(_storeRepository);

            
            Receipt receipt = new Receipt();

            foreach (KeyValuePair<int, int> cartItem in CartItems)
            {
                if (!products.ContainsKey(cartItem.Key))
                {
                    receipt.IgnoredItems.Add(_cartItemDescription[cartItem.Key]);
                    continue;
                }
                
                IProduct currentProduct = products[cartItem.Key];

                decimal bestPrice = cartItem.Value * products[cartItem.Key].Price;
                ReceiptLineItem receiptLineItem = new ReceiptLineItem(currentProduct.PLU, currentProduct.Name, cartItem.Value, currentProduct.Price);
                receipt.AddLineItem(receiptLineItem);


                decimal price = salePrice.Apply(cartItem, products[cartItem.Key].Price);

                if (price < bestPrice)
                {
                    bestPrice = price;
                    receiptLineItem.SetDiscountLine($"Sale @ ${salePrice.GetSalePrice(currentProduct.PLU)} ea ", bestPrice);
                }

                price = promotionalPrice.Apply(cartItem, products[cartItem.Key].Price);
                if (price < bestPrice )
                {
                    bestPrice = price;
                    receiptLineItem.SetDiscountLine($"Promotion ${promotionalPrice.GetAppliedPromotion(currentProduct.PLU)}", bestPrice);
                }

                receipt.GrandTotal += bestPrice;
            }

            return receipt;
        }

        public void Scan(string fileName)
        {
            string[] scannedItems = File.ReadAllLines(fileName);
            foreach (string item in scannedItems)
            {
                string[] record = item.Split('|');
                if (record.Length == 2)
                {
                    int plu = Convert.ToInt32(record[0]);
                    string desc = record[1];
                    AddItem(new CartItem(plu,desc));
                }
            }
        }

        internal Cart(IStoreRepository storeRepository)
        {
            CartItems = new Dictionary<int, int>();
            _storeRepository = storeRepository;
            _cartItemDescription = new Dictionary<int, ICartItem>();
        }

        public Cart()
        {
            CartItems = new Dictionary<int, int>();
            _storeRepository = new StoreRepository();
            _cartItemDescription = new Dictionary<int, ICartItem>();
        }
    }
}